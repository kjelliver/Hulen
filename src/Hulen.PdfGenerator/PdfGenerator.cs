using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using Hulen.BusinessServices.Interfaces;
using Hulen.PdfGenerator.Generators;
using iTextSharp.text.pdf;

namespace Hulen.PdfGenerator
{
    public class PdfGenerator : IPdfGenerator 
    {
        private readonly IResultService _resultService;
        private readonly IBudgetService _budgetService;
        private readonly IAccountInfoService _accountInfoService;

        public PdfGenerator(IResultService resultService, IBudgetService budgetService, IAccountInfoService accountInfoService)
        {
            _resultService = resultService;
            _accountInfoService = accountInfoService;
            _budgetService = budgetService;
        }


        public byte[] GetPdf(string context, Dictionary<string, string> dictionary)
        {
            if (context == "RESULT_REPORT")
                return GetResultReport(dictionary);
            return GetResultReport(dictionary);
        }

        private byte[] GetResultReport(Dictionary<string, string> dictionary)
        {
            var resultReport = new ResultReport(_resultService, _budgetService, _accountInfoService) {Dictionary = dictionary};
            return resultReport.GeneratePdf();
        }
    }
}