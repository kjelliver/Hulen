using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.ViewModels;
using iTextSharp.text.pdf;

namespace Hulen.PdfGenerator.Generators
{
    public class ResultReport
    {
        private readonly IResultService _resultService;
        private readonly IBudgetService _budgetService;
        private readonly IAccountInfoService _accountInfoService;

        private IEnumerable<AccountInfoViewModel> _accounts;
        private IEnumerable<BudgetAccountDTO> _budget;
        private IEnumerable<ResultAccountDTO> _result;

        private string _templatePath;
        private PdfReader _pdfReader;
        private MemoryStream _stream;
        private PdfStamper _stamper;

        public ResultReport(IResultService resultService, IBudgetService budgetService, IAccountInfoService accountInfoService)
        {
            _resultService = resultService;
            _accountInfoService = accountInfoService;
            _budgetService = budgetService;
        }

        public byte[] GeneratePdf()
        {
            GetRequiredData();
            SetPdfTemplatePath();
            InitStuff();

            FillPdfWithData();
           
            return CloseAndReturnStuff();  
        }

        private void GetRequiredData()
        {
            _result = _resultService.GetAllResultAccountsByYearAndPeriod(Convert.ToInt32(Dictionary["Year"]), Dictionary["Period"]);
            _budget = _budgetService.GetAllBudgetAccountsByYearAndStatus(Convert.ToInt32(Dictionary["Year"]), Dictionary["UsedBudget"]);
            _accounts = _accountInfoService.GetAllAccountInfosByYear(Convert.ToInt32(Dictionary["Year"]));
        }

        private void SetPdfTemplatePath()
        {
            var serverAddress = @HttpContext.Current.Server.MapPath("~");
            var templateAddress = @"Content\PdfTemplates\ResultReport" + Dictionary["Year"] + ".pdf";
            _templatePath = serverAddress + templateAddress;
        }

        private void InitStuff()
        {
            _pdfReader = new PdfReader(_templatePath);
            _stream = new MemoryStream();
            _stamper = new PdfStamper(_pdfReader, _stream);
        }

        private void FillPdfWithData()
        {
            AcroFields pdfFormFields = _stamper.AcroFields;

            //StampTitles();
            StampBudgetForThisPeriod(pdfFormFields);
            //StampResultForThisPeriod(pdfFormFields);
            //StampBudgetForSoFarThisYear(pdfFormFields);
            //StampResultForSoFarThisYear(pdfFormFields);
            //StampAvanseSection(pdfFormFields);

            _stamper.FormFlattening = true;
        }

        private byte[] CloseAndReturnStuff()
        {
            _pdfReader.Close();
            _stamper.Close();
            _stream.Flush();
            _stream.Close();
            return _stream.ToArray();
        }

        public Dictionary<string, string> Dictionary { get; set; }

        private void StampBudgetForThisPeriod(AcroFields pdfFormFields)
        {
            foreach (int accountNumber in _accounts.Select(account => account.AccountNumber).ToList())
            {
                var fieldName = "budget_" + accountNumber.ToString();
                pdfFormFields.SetField(fieldName, String.Format("{0:0.00}", _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().JanuaryAmount));
            }

            pdfFormFields.SetField("budget_total_Salgsinntekter", String.Format("{0:0.00}", SumSection("BUDGET", new List<string> { "Salgsinntekter" })));
            pdfFormFields.SetField("budget_total_AndreInntekter", String.Format("{0:0.00}", SumSection("BUDGET", new List<string> { "AndreInntekter" })));
            pdfFormFields.SetField("budget_total_Varekjøp", String.Format("{0:0.00}", SumSection("BUDGET", new List<string> { "Varekjøp" })));
            pdfFormFields.SetField("budget_total_Personalkostnader", String.Format("{0:0.00}", SumSection("BUDGET", new List<string> { "Personalkostnader" })));
            pdfFormFields.SetField("budget_total_Driftskostnader", String.Format("{0:0.00}", SumSection("BUDGET", new List<string> { "Driftskostnader" })));

            pdfFormFields.SetField("budget_total_Incomes", String.Format("{0:0.00}", SumSection("BUDGET", new List<string> { "Salgsinntekter", "AndreInntekter" })));
            pdfFormFields.SetField("budget_netto_Finalsielle", String.Format("{0:0.00}", CalculateFinancial("BUDGET")));

            CalculateExtraordinaryAndResult(pdfFormFields, "BUDGET");
        }

        private void CalculateExtraordinaryAndResult(AcroFields pdfFormFields, string context)
        {
            var result = SumSection(context, new List<string> {"Salgsinntekter", "AndreInntekter"}) -
                         SumSection(context, new List<string> {"Varekjøp"}) -
                         SumSection(context, new List<string> {"Personalkostnader"}) -
                         SumSection(context, new List<string> {"Driftskostnader"}) - CalculateFinancial(context);
            var fieldName = context.ToLower() + "_";
            pdfFormFields.SetField(fieldName + "total_Result", String.Format("{0:0.00}", result));
            pdfFormFields.SetField(fieldName + "extra_income", String.Format("{0:0.00}", 0));
            pdfFormFields.SetField(fieldName + "extra_expences", String.Format("{0:0.00}", 0));
            pdfFormFields.SetField(fieldName + "extra_taxes", String.Format("{0:0.00}", 0));
            pdfFormFields.SetField(fieldName + "extra_taxes", String.Format("{0:0.00}", 0));
            pdfFormFields.SetField(fieldName + "netto_extras", String.Format("{0:0.00}", 0));
            pdfFormFields.SetField(fieldName + "final_result", String.Format("{0:0.00}", result));
        }

        private double CalculateFinancial(string context)
        {
            var sum = 0.0;
            foreach (var accountNumber in _accounts.Where(x => x.ResultReportCategory == "Finansielle"))
            {
                var fortegn = 0;
                if (accountNumber.IsIncome == "Inntekt")
                    fortegn = 1;
                else
                {
                    fortegn = -1;
                }

                if (context == "BUDGET")
                {
                    switch (Dictionary["Period"])
                    {
                        case "Januar":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().JanuaryAmount * fortegn;
                            break;
                        case "Februar":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().FebruaryAmount * fortegn;
                            break;
                        case "Mars":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().MarchAmount * fortegn;
                            break;
                        case "April":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().AprilAmount * fortegn;
                            break;
                        case "Mai":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().MayAmount * fortegn;
                            break;
                        case "Juni":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().JuneAmount * fortegn;
                            break;
                        case "Juli":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().JulyAmount * fortegn;
                            break;
                        case "August":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().AugustAmount * fortegn;
                            break;
                        case "September":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().SeptemberAmount * fortegn;
                            break;
                        case "Oktober":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().OctoberAmount * fortegn;
                            break;
                        case "November":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().NovemberAmount * fortegn;
                            break;
                        case "Desember":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().DecemberAmount * fortegn;
                            break;
                    }
                    if (context == "Result")
                    {
                        sum += _result.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().AmountSoFar * fortegn;
                    }
                }
            }
            return sum;
        }

        private double SumSection(string context, List<string> sections)
        {
            var sum = 0.0;
            foreach (var accountNumber in _accounts.Where(x => sections.Contains(x.ResultReportCategory)))
            {
                if(context == "BUDGET")
                {
                    switch (Dictionary["Period"])
                    {
                        case "Januar":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().JanuaryAmount;
                            break;
                        case "Februar":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().FebruaryAmount;
                            break;
                        case "Mars":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().MarchAmount;
                            break;
                        case "April":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().AprilAmount;
                            break;
                        case "Mai":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().MayAmount;
                            break;
                        case "Juni":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().JuneAmount;
                            break;
                        case "Juli":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().JulyAmount;
                            break;
                        case "August":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().AugustAmount;
                            break;
                        case "September":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().SeptemberAmount;
                            break;
                        case "Oktober":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().OctoberAmount;
                            break;
                        case "November":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().NovemberAmount;
                            break;
                        case "Desember":
                            sum += _budget.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().DecemberAmount;
                            break;
                    }
                    if (context == "Result")
                    {
                        sum += _result.Where(x => x.AccountNumber == accountNumber.AccountNumber).FirstOrDefault().AmountSoFar;
                    }
                }
            }
            return sum;
        }
    }
}