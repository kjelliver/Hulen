using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.ReportingServices.Reports;

namespace Hulen.ReportingServices
{
    public class ReportingServices : IReportingServices
    {
        public string GenerateCssStyle(string type)
        {
            return new AccountInfoReport().GenerateCssStyle(); 
        }

        public string GenerateCssStyleForResultReport(int month, int year, int budgetStatus)
        {
            return new ResultReport(month, year, budgetStatus).GenerateCssStyle();
        }

        public string GenerateHtmlBody(string type)
        {
            return new AccountInfoReport().GenerateHtmlBody();
        }

        public string GenerateHtmlBodyForResultReport(int month, int year, int budgetStatus)
        {
            return new ResultReport(month, year, budgetStatus).GenerateHtmlBody();
        }
    }
}
