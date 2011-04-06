using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Hulen.ReportingServices;
using Hulen.ReportingServices.Reports;
using Hulen.Web.Models;

namespace Hulen.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportingServices _reportService = new AccountInfoReport();

        public ActionResult Index(int year)
        {
            var model = new ReportModel();
            model.Year = year;
            model.HtmlBody = GenerateHtmlBody();
            model.CssStyle = GenerateCssStyle();

            return View(model);
        }

        private string GenerateCssStyle()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<style type=\"text/css\">");
            sb.AppendLine("h1 {color:red}");
            sb.AppendLine(".rowHeader { height:30px; background-color: #AAAAAA; font-weight: bold; }");
            sb.AppendLine(".columnAccNr { width:7%;}");
            sb.AppendLine(".columnAccName {width:24%;}");
            sb.AppendLine(".columnAccData {width:16%; text-align:center;}");
            sb.AppendLine(".columnAccYear {width:5%; text-align:center;}");
            sb.AppendLine("table {border: 1px solid black;}");
            sb.AppendLine("td {border: 1px solid black;}");
            sb.AppendLine("</style>");
            return sb.ToString();
        }

        private string GenerateHtmlBody()
        {
            return _reportService.GenerateHtmlBody();
        }
    }
}
