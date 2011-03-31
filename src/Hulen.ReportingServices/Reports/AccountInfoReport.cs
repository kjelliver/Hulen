using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Hulen.ReportingServices.Reports
{
    public class AccountInfoReport : IReportingServices
    {
        public Stream GeneratePDF()
        {
            MemoryStream outputStream = new MemoryStream();
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream);
            document.Open();
            document.Add(new Paragraph("Kontoinformasjon"));
            document.Add(new Paragraph(DateTime.Now.ToString()));
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            outputStream.Write(byteInfo, 0, byteInfo.Length);
            outputStream.Position = 0;

            return outputStream;
        }
    }
}
