using System.Collections.Generic;

namespace Hulen.PdfGenerator
{
    public interface IPdfGenerator
    {
        byte[] GetPdf(string context, Dictionary<string, string> dictionary);
    }
}