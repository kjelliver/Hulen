using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
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
            //_result = _resultService.GetAllResultAccountsByYearAndPeriod(2011, "Januar");
            _budget = _budgetService.GetAllBudgetAccountsByYearAndStatus(Convert.ToInt32(Dictionary["Year"]), Dictionary["UsedBudget"]);
            //_accounts = _accountInfoService.GetAllAccountNumbersByYear(2011);
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
            pdfFormFields.SetField("budget_3000", "3000");
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
    }
}