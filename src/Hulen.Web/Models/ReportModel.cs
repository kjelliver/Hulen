using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hulen.Web.Models
{
    public class ReportModel
    {
        public int Year { get; set; }
        public string Type { get; set; }
        public string HtmlBody { get; set; }
        public string CssStyle { get; set; }
    }
}