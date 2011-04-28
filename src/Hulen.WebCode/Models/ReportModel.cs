namespace Hulen.WebCode.Models
{
    public class ReportModel
    {
        //HTML
        public string HtmlBody { get; set; }
        public string CssStyle { get; set; }

        //AccountInfo
        public int AccountInfoYear { get; set; }

        //ResultReport
        public int ResultReportMonth { get; set; }
        public int ResultReportYear { get; set; }

        public string Type { get; set; }
        
    }
}