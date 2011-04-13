using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    public class FileImportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }

    public class AccountInfoImportController : Controller
    {
        private readonly IFileImportService _fileImportService;

        public AccountInfoImportController()
        {
            _fileImportService = new FileImportService();
        }

        public ActionResult Index()
        {
            var model = new FileImportWebModel();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(HttpPostedFileBase uploadFile, AccountInfoImportWebModel model)
        {
            if (uploadFile.ContentLength > 0)
            {
                _fileImportService.ImportFile("ACCOUNT_INFO", uploadFile.InputStream, model.AccountInfoYear);
            }
            return RedirectToAction("Index", "FileImport");
        }
    }

    public class BudgetImportController : Controller
    {
        private readonly IFileImportService _fileImportService;

        public BudgetImportController()
        {
            _fileImportService = new FileImportService();
        }

        public ActionResult Index()
        {
            var model = new BudgetImportWebModel();
            model.BudgetStatusList = new List<string> { "Orginalt", "Revidert" };
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(HttpPostedFileBase uploadFile, BudgetImportWebModel model)
        {
            if (uploadFile.ContentLength > 0)
            {
                _fileImportService.ImportFile("BUDGET_YEAR", uploadFile.InputStream, model.BudgetYear, model.BudgetStatus);
            }
            return RedirectToAction("Index", "FileImport");
        }
    }
}