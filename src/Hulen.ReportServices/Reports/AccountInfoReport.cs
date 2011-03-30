

using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Hulen.ReportServices.Reports
{
    class AccountInfoReport
    {
        public Stream GeneratePdf()
        {
            MemoryStream outputStream = new MemoryStream();
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream);
            document.Open();
            document.Add(new Paragraph("Hello World"));
            document.Add(new Paragraph(DateTime.Now.ToString()));
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            outputStream.Write(byteInfo, 0, byteInfo.Length);
            outputStream.Position = 0;

            return outputStream;
        }
    }
}
