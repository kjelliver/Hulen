using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hulen.ReportServices.Reports;

namespace Hulen.ReportServices.Service
{
    public class ReportingServices : IReportingServices
    {
        public Stream GeneratePDF(string reportType)
        {
            var rapport = new AccountInfoReport();
            return rapport.GeneratePdf();
        }
    }
}
