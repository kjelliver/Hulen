using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;

namespace Hulen.PdfGenerator
{
    public class Class1
    {
        public MemoryStream FillPdf()
        {
            string pdfTemplate = @"C:\Users\Kjell Iver\Dropbox\Utvikling\Projects\Hulen\data\test.pdf";
            PdfReader pdfReader = new PdfReader(pdfTemplate);
            MemoryStream stream = new MemoryStream(); 
            PdfStamper pdfStamper = new PdfStamper(pdfReader, stream);
            AcroFields pdfFormFields = pdfStamper.AcroFields;

            //pdfFormFields.SetField("EN_TEST", "Hei på deg!");
           
            pdfStamper.FormFlattening = false;

            pdfStamper.Close();

            return stream;

            MemoryStream ms = new MemoryStream();

            byte[] byteInfo = stream.ToArray();
            ms.Write(byteInfo, 0, byteInfo.Length);
            ms.Position = 0;

            return ms;
        }

    }
}
