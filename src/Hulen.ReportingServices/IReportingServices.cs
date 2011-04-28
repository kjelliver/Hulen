using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hulen.ReportingServices
{
    public interface IReportingServices
    {
        string GenerateCssStyle(string type);
        string GenerateCssStyleForResultReport(int month, int year, int budgetStatus);
        string GenerateHtmlBody(string type);
        string GenerateHtmlBodyForResultReport(int month, int year, int budgetStatus);
    }
}
