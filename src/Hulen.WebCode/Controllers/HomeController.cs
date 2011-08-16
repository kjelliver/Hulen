using System.Web.Mvc;
using Hulen.WebCode.Attributes;

namespace Hulen.WebCode.Controllers
{
    [HandleError]
    [HulenAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }
    }
}
