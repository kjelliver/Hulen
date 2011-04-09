using System.Web.Mvc;
using Hulen.ReportingServices;
using Hulen.ReportingServices.Reports;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
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
            return _reportService.GenerateCssStyle();
        }

        private string GenerateHtmlBody()
        {
            return _reportService.GenerateHtmlBody();
        }
    }
}
