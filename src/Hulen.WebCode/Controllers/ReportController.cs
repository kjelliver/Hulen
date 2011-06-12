using System.Web.Mvc;
using Hulen.ReportingServices;
using Hulen.ReportingServices.Reports;
using Hulen.WebCode.Attributes;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    [HulenAuthorize]
    public class ReportController : Controller
    {
        private readonly IReportingServices _reportService = new ReportingServices.ReportingServices();

        public ReportController(IReportingServices reportService)
        {
            _reportService = reportService;
        }

        public ActionResult Index()
        {
            return View(new ReportModel());
        }

        public ActionResult AccountInfo(ReportModel model)
        {
            model.HtmlBody = GenerateHtmlBody("ACCOUNTINFO");
            model.CssStyle = GenerateCssStyle("ACCOUNTINFO");

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResultReport(ReportModel model)
        {
            model.HtmlBody = _reportService.GenerateHtmlBodyForResultReport(model.ResultReportMonth, model.ResultReportYear, 0);
            model.CssStyle = _reportService.GenerateCssStyleForResultReport(model.ResultReportMonth, model.ResultReportYear, 0);
            return View(model);
        }

        private string GenerateCssStyle(string type)
        {
            return _reportService.GenerateCssStyle(type);
        }

        private string GenerateHtmlBody(string type)
        {
            return _reportService.GenerateHtmlBody(type);
        }
    }
}
