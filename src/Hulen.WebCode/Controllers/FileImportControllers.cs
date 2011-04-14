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
        private readonly IAccountInfoService _accountInfoImportService;

        public AccountInfoImportController()
        {
            _accountInfoImportService = new AccountInfoService() ;
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
                _accountInfoImportService.ImportFile(uploadFile.InputStream, model.AccountInfoYear);
            }
            return RedirectToAction("Index", "FileImport");
        }
    }

    public class BudgetImportController : Controller
    {
        private readonly IBudgetService _budgetImportService;

        public BudgetImportController()
        {
            _budgetImportService = new BudgetService();
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
                _budgetImportService.ImportFile(uploadFile.InputStream, model.BudgetYear, model.BudgetStatus);
            }
            return RedirectToAction("Index", "FileImport");
        }
    }

    public class ResultAccountImportController : Controller
    {
        private readonly IResultAccountService _resultAccountService;

        public ResultAccountImportController()
        {
            _resultAccountService = new ResultAccountService();
        }

        public ActionResult Index()
        {
            var model = new ResultAccountImportWebModel();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(HttpPostedFileBase uploadFile, ResultAccountImportWebModel model)
        {
            if (uploadFile.ContentLength > 0)
            {
                _resultAccountService.ImportFile(uploadFile.InputStream, model.Month, model.ResultYear);
            }
            return RedirectToAction("Index", "FileImport");
        }
    }
}