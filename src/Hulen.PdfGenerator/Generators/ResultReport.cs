using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Objects.Models;
using iTextSharp.text.pdf;

namespace Hulen.PdfGenerator.Generators
{
    public class ResultReport
    {
        private readonly IResultService _resultService;
        private readonly IBudgetService _budgetService;
        private readonly IAccountInfoService _accountInfoService;

        private IEnumerable<AccountInfo> _accounts;
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

            StampTitles(pdfFormFields);
            StampThisPeriod(pdfFormFields, "BUDGET");
            StampThisPeriod(pdfFormFields, "RESULT");
            StampThisPeriod(pdfFormFields, "RESULT_SOFAR");
            StampThisPeriod(pdfFormFields, "BUDGET_SOFAR");
            StampProfitSection(pdfFormFields);

            _stamper.FormFlattening = true;
        }

        private void StampProfitSection(AcroFields pdfFormFields)
        {
            StampPartsResult(pdfFormFields);
            StampProfitMargin(pdfFormFields);
        }

        private void StampProfitMargin(AcroFields pdfFormFields)
        {
            pdfFormFields.SetField("BUDSJETTTankøl", String.Format("{0:0.00}", CalculateProfitMargin("BUDGET", 3010, 4310)));
            pdfFormFields.SetField("BUDSJETTFlaskeøl", String.Format("{0:0.00}", CalculateProfitMargin("BUDGET", 3015, 4315)));
            pdfFormFields.SetField("BUDSJETTVin", String.Format("{0:0.00}", CalculateProfitMargin("BUDGET", 3000, 4320)));
            pdfFormFields.SetField("BUDSJETTLavprosent_sprit", String.Format("{0:0.00}", CalculateProfitMargin("BUDGET", 3016, 4325)));
            pdfFormFields.SetField("BUDSJETTSigaretter", String.Format("{0:0.00}", CalculateProfitMargin("BUDGET", 3004, 4350)));

            pdfFormFields.SetField("REGNSKAPTankøl", String.Format("{0:0.00}", CalculateProfitMargin("RESULT", 3010, 4310)));
            pdfFormFields.SetField("REGNSKAPFlaskeøl", String.Format("{0:0.00}", CalculateProfitMargin("RESULT", 3015, 4315)));
            pdfFormFields.SetField("REGNSKAPVin", String.Format("{0:0.00}", CalculateProfitMargin("RESULT", 3000, 4320)));
            pdfFormFields.SetField("REGNSKAPLavprosent_sprit", String.Format("{0:0.00}", CalculateProfitMargin("RESULT", 3016, 4325)));
            pdfFormFields.SetField("REGNSKAPSigaretter", String.Format("{0:0.00}", CalculateProfitMargin("RESULT", 3004, 4350)));

            pdfFormFields.SetField("BUDSJETTTankøl_2", String.Format("{0:0.00}", CalculateProfitMargin("BUDGET_SOFAR", 3010, 4310)));
            pdfFormFields.SetField("BUDSJETTFlaskeøl_2", String.Format("{0:0.00}", CalculateProfitMargin("BUDGET_SOFAR", 3015, 4315)));
            pdfFormFields.SetField("BUDSJETTVin_2", String.Format("{0:0.00}", CalculateProfitMargin("BUDGET_SOFAR", 3000, 4320)));
            pdfFormFields.SetField("BUDSJETTLavprosent_sprit_2", String.Format("{0:0.00}", CalculateProfitMargin("BUDGET_SOFAR", 3016, 4325)));
            pdfFormFields.SetField("BUDSJETTSigaretter_2", String.Format("{0:0.00}", CalculateProfitMargin("BUDGET_SOFAR", 3004, 4350)));

            pdfFormFields.SetField("REGNSKAPTankøl_2", String.Format("{0:0.00}", CalculateProfitMargin("RESULT_SOFAR", 3010, 4310)));
            pdfFormFields.SetField("REGNSKAPFlaskeøl_2", String.Format("{0:0.00}", CalculateProfitMargin("RESULT_SOFAR", 3015, 4315)));
            pdfFormFields.SetField("REGNSKAPVin_2", String.Format("{0:0.00}", CalculateProfitMargin("RESULT_SOFAR", 3000, 4320)));
            pdfFormFields.SetField("REGNSKAPLavprosent_sprit_2", String.Format("{0:0.00}", CalculateProfitMargin("RESULT_SOFAR", 3016, 4325)));
            pdfFormFields.SetField("REGNSKAPSigaretter_2", String.Format("{0:0.00}", CalculateProfitMargin("RESULT_SOFAR", 3004, 4350)));

            StampTotalProfitMargin(pdfFormFields);

        }

        private void StampTotalProfitMargin(AcroFields pdfFormFields)
        {
            //BUDGET
            var budgetIncome = SumSection("BUDGET", new List<string> {"Salgsinntekter"});
            var budgetCosts =  SumSection("BUDGET", new List<string> {"Varekjøp"});
            pdfFormFields.SetField("BUDSJETTTotalt", String.Format("{0:0.00}", ((budgetIncome - budgetCosts) * 100) / budgetIncome));
            pdfFormFields.SetField("BUDSJETTTotalt_2", String.Format("{0:0.00}", ((budgetIncome - budgetCosts)*100)/budgetIncome));

            //RESULT
            var resultPeriodIncome = SumSection("RESULT", new List<string> { "Salgsinntekter" });
            var resultPeriodCosts = SumSection("RESULT", new List<string> { "Varekjøp" });
            pdfFormFields.SetField("REGNSKAPTotalt", String.Format("{0:0.00}", ((resultPeriodIncome - resultPeriodCosts) * 100) / resultPeriodIncome));

            //RESULT_SOFAR
            var resultSofarIncome = SumSection("RESULT_SOFAR", new List<string> { "Salgsinntekter" });
            var resultSofarCosts = SumSection("RESULT_SOFAR", new List<string> { "Varekjøp" });
            pdfFormFields.SetField("REGNSKAPTotalt_2", String.Format("{0:0.00}", ((resultSofarIncome - resultSofarCosts) * 100) / resultSofarIncome));
        }

        private double CalculateProfitMargin(string context, int income, int costs)
        {
            if (context == "BUDGET")
            {
                switch (Dictionary["Period"])
                {
                    case "Januar":
                        return ((_budget.Where(x => x.AccountNumber == income).FirstOrDefault().JanuaryAmount - _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().JanuaryAmount) * 100) / _budget.Where(x => x.AccountNumber == income).FirstOrDefault().JanuaryAmount;
                    case "Februar":
                        return ((_budget.Where(x => x.AccountNumber == income).FirstOrDefault().FebruaryAmount - _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().FebruaryAmount) * 100) / _budget.Where(x => x.AccountNumber == income).FirstOrDefault().FebruaryAmount;
                    case "Mars":
                        return ((_budget.Where(x => x.AccountNumber == income).FirstOrDefault().MarchAmount - _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().MarchAmount) * 100) / _budget.Where(x => x.AccountNumber == income).FirstOrDefault().MarchAmount;
                    case "April":
                        return ((_budget.Where(x => x.AccountNumber == income).FirstOrDefault().AprilAmount - _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().AprilAmount) * 100) / _budget.Where(x => x.AccountNumber == income).FirstOrDefault().AprilAmount;
                    case "Mai":
                        return ((_budget.Where(x => x.AccountNumber == income).FirstOrDefault().MayAmount - _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().MayAmount) * 100) / _budget.Where(x => x.AccountNumber == income).FirstOrDefault().MayAmount;
                    case "Juni":
                        return ((_budget.Where(x => x.AccountNumber == income).FirstOrDefault().JuneAmount - _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().JuneAmount) * 100) / _budget.Where(x => x.AccountNumber == income).FirstOrDefault().JuneAmount;
                    case "Juli":
                        return ((_budget.Where(x => x.AccountNumber == income).FirstOrDefault().JulyAmount - _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().JulyAmount) * 100) / _budget.Where(x => x.AccountNumber == income).FirstOrDefault().JulyAmount;
                    case "August":
                        return ((_budget.Where(x => x.AccountNumber == income).FirstOrDefault().AugustAmount - _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().AugustAmount) * 100) / _budget.Where(x => x.AccountNumber == income).FirstOrDefault().AugustAmount;
                    case "September":
                        return ((_budget.Where(x => x.AccountNumber == income).FirstOrDefault().SeptemberAmount - _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().SeptemberAmount) * 100) / _budget.Where(x => x.AccountNumber == income).FirstOrDefault().SeptemberAmount;
                    case "Oktober":
                        return ((_budget.Where(x => x.AccountNumber == income).FirstOrDefault().OctoberAmount - _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().OctoberAmount) * 100) / _budget.Where(x => x.AccountNumber == income).FirstOrDefault().OctoberAmount;
                    case "November":
                        return ((_budget.Where(x => x.AccountNumber == income).FirstOrDefault().NovemberAmount - _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().NovemberAmount) * 100) / _budget.Where(x => x.AccountNumber == income).FirstOrDefault().NovemberAmount;
                    case "Desember":
                        return ((_budget.Where(x => x.AccountNumber == income).FirstOrDefault().DecemberAmount - _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().DecemberAmount) * 100) / _budget.Where(x => x.AccountNumber == income).FirstOrDefault().DecemberAmount;
                }
            }
            if (context == "RESULT")
            {
                return ((_result.Where(x => x.AccountNumber == income).Sum(a => a.AmountMonth) - _result.Where(x => x.AccountNumber == costs).Sum(a => a.AmountMonth)) * 100) / _result.Where(x => x.AccountNumber == income).Sum(a => a.AmountMonth);
            }
            if (context == "RESULT_SOFAR")
            {
                return ((_result.Where(x => x.AccountNumber == income).Sum(a => a.AmountSoFar) - _result.Where(x => x.AccountNumber == costs).Sum(a => a.AmountSoFar)) * 100) / _result.Where(x => x.AccountNumber == income).Sum(a => a.AmountSoFar);
            }
            if (context == "BUDGET_SOFAR")
            {
               return CalculateBudgetProfitMarginSoFar(income, costs);
            }
            return 0.0;
        }

        private double CalculateBudgetProfitMarginSoFar(int income, int costs)
        {
            double totalIncome = 0.0;
            double totalCosts = 0.0;
            int period = (int)Enum.Parse(typeof(ResultPeriod), Dictionary["Period"]);
            if (period <= 1)
            {
                totalIncome += _budget.Where(x => x.AccountNumber == income).FirstOrDefault().JanuaryAmount;
                totalCosts += _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().JanuaryAmount;                
            }
            if (period <= 2)
            {
                totalIncome += _budget.Where(x => x.AccountNumber == income).FirstOrDefault().FebruaryAmount;
                totalCosts += _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().FebruaryAmount; 
            }
            if (period <= 3)
            {
                totalIncome += _budget.Where(x => x.AccountNumber == income).FirstOrDefault().MarchAmount;
                totalCosts += _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().MarchAmount;
            }
            if (period <= 4)
            {
                totalIncome += _budget.Where(x => x.AccountNumber == income).FirstOrDefault().AprilAmount;
                totalCosts += _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().AprilAmount;
            }
            if (period <= 5)
            {
                totalIncome += _budget.Where(x => x.AccountNumber == income).FirstOrDefault().MayAmount;
                totalCosts += _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().MayAmount;
            }
            if (period <= 6)
            {
                totalIncome += _budget.Where(x => x.AccountNumber == income).FirstOrDefault().JuneAmount;
                totalCosts += _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().JuneAmount;
            }
            if (period <= 7)
            {
                totalIncome += _budget.Where(x => x.AccountNumber == income).FirstOrDefault().JulyAmount;
                totalCosts += _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().JulyAmount;
            }
            if (period <= 8)
            {
                totalIncome += _budget.Where(x => x.AccountNumber == income).FirstOrDefault().AugustAmount;
                totalCosts += _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().AugustAmount;
            }
            if (period <= 9)
            {
                totalIncome += _budget.Where(x => x.AccountNumber == income).FirstOrDefault().SeptemberAmount;
                totalCosts += _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().SeptemberAmount;
            }
            if (period <= 10)
            {
                totalIncome += _budget.Where(x => x.AccountNumber == income).FirstOrDefault().OctoberAmount;
                totalCosts += _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().OctoberAmount;
            }
            if (period <= 11)
            {
                totalIncome += _budget.Where(x => x.AccountNumber == income).FirstOrDefault().NovemberAmount;
                totalCosts += _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().NovemberAmount;
            }
            if (period <= 12)
            {
                totalIncome += _budget.Where(x => x.AccountNumber == income).FirstOrDefault().DecemberAmount;
                totalCosts += _budget.Where(x => x.AccountNumber == costs).FirstOrDefault().DecemberAmount;
            }
            return ((totalIncome - totalCosts)*100)/totalIncome;
        }

        private void StampPartsResult(AcroFields pdfFormFields)
        {
            pdfFormFields.SetField("BUDSJETTBardrift", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET", new List<string> { "Bar" })));
            pdfFormFields.SetField("BUDSJETTArrangementsdrift", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET", new List<string> { "Arrangement" })));
            pdfFormFields.SetField("BUDSJETTPersonalkostnader", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET", new List<string> { "Personalkostnader" })));
            pdfFormFields.SetField("BUDSJETTPublic_Relations", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET", new List<string> { "PublicRelations" })));
            pdfFormFields.SetField("BUDSJETTTilskudd", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET", new List<string> { "Tilskudd" })));
            pdfFormFields.SetField("BUDSJETTØkonomi", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET", new List<string> { "Økonomi" })));
            pdfFormFields.SetField("BUDSJETTDriftskostnader", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET", new List<string> { "Driftskostnader" })));

            pdfFormFields.SetField("REGNSKAPBardrift", String.Format("{0:0.00}", SumPartResultPeriod("RESULT", new List<string> { "Bar" })));
            pdfFormFields.SetField("REGNSKAPArrangementsdrift", String.Format("{0:0.00}", SumPartResultPeriod("RESULT", new List<string> { "Arrangement" })));
            pdfFormFields.SetField("REGNSKAPPersonalkostnader", String.Format("{0:0.00}", SumPartResultPeriod("RESULT", new List<string> { "Personalkostnader" })));
            pdfFormFields.SetField("REGNSKAPPublic_Relations", String.Format("{0:0.00}", SumPartResultPeriod("RESULT", new List<string> { "PublicRelations" })));
            pdfFormFields.SetField("REGNSKAPTilskudd", String.Format("{0:0.00}", SumPartResultPeriod("RESULT", new List<string> { "Tilskudd" })));
            pdfFormFields.SetField("REGNSKAPØkonomi", String.Format("{0:0.00}", SumPartResultPeriod("RESULT", new List<string> { "Økonomi" })));
            pdfFormFields.SetField("REGNSKAPDriftskostnader", String.Format("{0:0.00}", SumPartResultPeriod("RESULT", new List<string> { "Driftskostnader" })));

            pdfFormFields.SetField("BUDSJETTBardrift_2", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET_SOFAR", new List<string> { "Bar" })));
            pdfFormFields.SetField("BUDSJETTArrangementsdrift_2", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET_SOFAR", new List<string> { "Arrangement" })));
            pdfFormFields.SetField("BUDSJETTPersonalkostnader_2", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET_SOFAR", new List<string> { "Personalkostnader" })));
            pdfFormFields.SetField("BUDSJETTPublic_Relations_2", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET_SOFAR", new List<string> { "PublicRelations" })));
            pdfFormFields.SetField("BUDSJETTTilskudd_2", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET_SOFAR", new List<string> { "Tilskudd" })));
            pdfFormFields.SetField("BUDSJETTØkonomi_2", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET_SOFAR", new List<string> { "Økonomi" })));
            pdfFormFields.SetField("BUDSJETTDriftskostnader_2", String.Format("{0:0.00}", SumPartResultPeriod("BUDGET_SOFAR", new List<string> { "Driftskostnader" })));

            pdfFormFields.SetField("REGNSKAPBardrift_2", String.Format("{0:0.00}", SumPartResultPeriod("RESULT_SOFAR", new List<string> { "Bar" })));
            pdfFormFields.SetField("REGNSKAPArrangementsdrift_2", String.Format("{0:0.00}", SumPartResultPeriod("RESULT_SOFAR", new List<string> { "Arrangement" })));
            pdfFormFields.SetField("REGNSKAPPersonalkostnader_2", String.Format("{0:0.00}", SumPartResultPeriod("RESULT_SOFAR", new List<string> { "Personalkostnader" })));
            pdfFormFields.SetField("REGNSKAPPublic_Relations_2", String.Format("{0:0.00}", SumPartResultPeriod("RESULT_SOFAR", new List<string> { "PublicRelations" })));
            pdfFormFields.SetField("REGNSKAPTilskudd_2", String.Format("{0:0.00}", SumPartResultPeriod("RESULT_SOFAR", new List<string> { "Tilskudd" })));
            pdfFormFields.SetField("REGNSKAPØkonomi_2", String.Format("{0:0.00}", SumPartResultPeriod("RESULT_SOFAR", new List<string> { "Økonomi" })));
            pdfFormFields.SetField("REGNSKAPDriftskostnader_2", String.Format("{0:0.00}", SumPartResultPeriod("RESULT_SOFAR", new List<string> { "Driftskostnader" })));
        }

        private double SumPartResultPeriod(string context, List<string> sections)
        {
            var sum = 0.0;
            foreach (var accountNumber in _accounts.Where(x => sections.Contains(x.PartsReportCategory)))
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
                }
                if (context == "RESULT")
                {
                    sum += _result.Where(x => x.AccountNumber == accountNumber.AccountNumber).Sum(a => a.AmountMonth) * fortegn;
                }

                if (context == "RESULT_SOFAR")
                {
                    sum += _result.Where(x => x.AccountNumber == accountNumber.AccountNumber).Sum(a => a.AmountSoFar) * fortegn;
                }
                if (context == "BUDGET_SOFAR")
                {
                    sum += CalculateBudgetSoFar(accountNumber.AccountNumber) * fortegn;
                }
            }
            return sum;
        }

        private void StampTitles(AcroFields pdfFormFields)
        {
            string period = Dictionary["Period"] + ", " + Dictionary["Year"];
            pdfFormFields.SetField("period1", period);
            pdfFormFields.SetField("period2", period);
            pdfFormFields.SetField("period3", period);

            pdfFormFields.SetField("title1", "Resultatrapport");
            pdfFormFields.SetField("title2", "Fortjenesteoversikt");
        }

        private byte[] CloseAndReturnStuff()
        {
            _pdfReader.Close();
            _stamper.Close();
            _stream.Flush();
            _stream.Close();
            return _stream.ToArray();
        }

        private void StampThisPeriod(AcroFields pdfFormFields, string context)
        {
            var field = context.ToLower() + "_";

            foreach (int accountNumber in _accounts.Select(account => account.AccountNumber).ToList())
            {
                var fieldName = field + accountNumber.ToString();
                if(context == "BUDGET")
                {
                    pdfFormFields.SetField(fieldName, String.Format("{0:0.00}", FindBudgetThisMonthForAccount(accountNumber)));                    
                }
                if(context == "RESULT")
                {
                    if (_result.Select(accountNumbersInResult => accountNumbersInResult.AccountNumber).ToList().Contains(accountNumber))
                    {
                        pdfFormFields.SetField(fieldName, String.Format("{0:0.00}", _result.Where(x => x.AccountNumber == accountNumber).Sum(a=>a.AmountMonth)));
                    }
                    else
                    {
                        pdfFormFields.SetField(fieldName, String.Format("{0:0.00}", 0));
                    }
                }
                if(context == "BUDGET_SOFAR")
                {
                    pdfFormFields.SetField(fieldName, String.Format("{0:0.00}", CalculateBudgetSoFar(accountNumber)));                    
                    
                }

                if (context == "RESULT_SOFAR")
                {
                    if (_result.Select(accountNumbersInResult => accountNumbersInResult.AccountNumber).ToList().Contains(accountNumber))
                    {
                        pdfFormFields.SetField(fieldName, String.Format("{0:0.00}", _result.Where(x => x.AccountNumber == accountNumber).Sum(a => a.AmountSoFar)));
                    }
                    else
                    {
                        pdfFormFields.SetField(fieldName, String.Format("{0:0.00}", 0));
                    }
                }
            }

            pdfFormFields.SetField(field + "total_Salgsinntekter", String.Format("{0:0.00}", SumSection(context, new List<string> { "Salgsinntekter" })));
            pdfFormFields.SetField(field + "total_AndreInntekter", String.Format("{0:0.00}", SumSection(context, new List<string> { "AndreInntekter" })));
            pdfFormFields.SetField(field + "total_Varekjøp", String.Format("{0:0.00}", SumSection(context, new List<string> { "Varekjøp" })));
            pdfFormFields.SetField(field + "total_Personalkostnader", String.Format("{0:0.00}", SumSection(context, new List<string> { "Personalkostnader" })));
            pdfFormFields.SetField(field + "total_Driftskostnader", String.Format("{0:0.00}", SumSection(context, new List<string> { "Driftskostnader" })));

            pdfFormFields.SetField(field + "total_Incomes", String.Format("{0:0.00}", SumSection(context, new List<string> { "Salgsinntekter", "AndreInntekter" })));
            pdfFormFields.SetField(field + "netto_Finalsielle", String.Format("{0:0.00}", CalculateFinancial(context)));

            CalculateExtraordinaryAndResult(pdfFormFields, context);
        }

        private double FindBudgetThisMonthForAccount(int accountNumber)
        {
            int period = (int)Enum.Parse(typeof(ResultPeriod), Dictionary["Period"]);
            if (period == 1)
                return _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().JanuaryAmount;
            if (period == 2)
                return _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().FebruaryAmount;
            if (period == 3)
                return _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().MarchAmount;
            if (period == 4)
                return _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().AprilAmount;
            if (period == 5)
                return _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().MayAmount;
            if (period == 6)
                return _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().JuneAmount;
            if (period == 7)
                return _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().JulyAmount;
            if (period == 8)
                return _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().AugustAmount;
            if (period == 9)
                return _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().SeptemberAmount;
            if (period == 10)
                return _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().OctoberAmount;
            if (period == 11)
                return _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().NovemberAmount;
            if (period == 12 || period == 0)
                return _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().DecemberAmount;
            return 0.0;
        }

        private double CalculateBudgetSoFar(int accountNumber)
        {
            double sum = 0.0;
            int period = (int) Enum.Parse(typeof (ResultPeriod), Dictionary["Period"]);
            if(period >= 1)
                sum += _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().JanuaryAmount;
            if (period >= 2)
                sum += _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().FebruaryAmount;
            if (period >= 3)
                sum += _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().MarchAmount;
            if (period >= 4)
                sum += _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().AprilAmount ;
            if (period >= 5)
                sum += _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().MayAmount;
            if (period >= 6)
                sum += _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().JuneAmount;
            if (period >= 7)
                sum += _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().JulyAmount;
            if (period >= 8)
                sum += _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().AugustAmount;
            if (period >= 9)
                sum += _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().SeptemberAmount;
            if (period >= 10)
                sum += _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().OctoberAmount;
            if (period >= 11)
                sum += _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().NovemberAmount;
            if (period >= 12)
                sum += _budget.Where(x => x.AccountNumber == accountNumber).FirstOrDefault().DecemberAmount;
            return sum;
        }

        private void CalculateExtraordinaryAndResult(AcroFields pdfFormFields, string context)
        {
            var result = SumSection(context, new List<string> {"Salgsinntekter", "AndreInntekter"}) -
                         SumSection(context, new List<string> {"Varekjøp"}) -
                         SumSection(context, new List<string> {"Personalkostnader"}) -
                         SumSection(context, new List<string> {"Driftskostnader"}) + CalculateFinancial(context);
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
                }
                if (context == "RESULT")
                {
                    sum += _result.Where(x => x.AccountNumber == accountNumber.AccountNumber).Sum(a => a.AmountMonth) * fortegn;
                }

                if (context == "RESULT_SOFAR")
                {
                    sum += _result.Where(x => x.AccountNumber == accountNumber.AccountNumber).Sum(a => a.AmountSoFar) * fortegn;
                }
                if (context == "BUDGET_SOFAR")
                {
                    sum += CalculateBudgetSoFar(accountNumber.AccountNumber) * fortegn;
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
                }
                if (context == "RESULT")
                {
                    sum += _result.Where(x => x.AccountNumber == accountNumber.AccountNumber).Sum(a => a.AmountMonth);
                }

                if (context == "RESULT_SOFAR")
                {
                    sum += _result.Where(x => x.AccountNumber == accountNumber.AccountNumber).Sum(a => a.AmountSoFar);
                }
                if (context == "BUDGET_SOFAR")
                {
                    sum += CalculateBudgetSoFar(accountNumber.AccountNumber);
                }
            }
            return sum;
        }

        public Dictionary<string, string> Dictionary { get; set; }

    }
}