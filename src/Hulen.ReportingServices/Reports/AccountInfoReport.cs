using System.IO;

namespace Hulen.ReportingServices.Reports
{
    public class AccountInfoReport : IReportingServices
    {
        public Stream GeneratePDF()
        {
            return new MemoryStream();
        }
    }
}
