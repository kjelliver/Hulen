using System.Web;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    public class FileImportController : Controller
    {
        private readonly IFileImportService _fileImportService = new FileImportService();

        public ActionResult Index()
        {
            var model = new FileImportWebModel();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(HttpPostedFileBase uploadFile, FileImportWebModel model)
        {
            if (uploadFile.ContentLength > 0)
            {
                _fileImportService.ImportFile("ACCOUNT_INFO", uploadFile.InputStream, model.AccountInfoYear);
            }
            return RedirectToAction("Index");
        }
    }
}