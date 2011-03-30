using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hulen.ReportServices
{
    public interface IReportingServices
    {
        Stream GeneratePDF(string reportType);
    }
}
