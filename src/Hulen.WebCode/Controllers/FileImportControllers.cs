using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.Objects.DTO;
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
                model.FailedAccountsCollection = _resultAccountService.TryToImportFile(uploadFile.InputStream, model.Month, model.ResultYear);
            }
            if (model.FailedAccountsCollection.Count() < 1)
                return RedirectToAction("Index", "FileImport");
            else
                model.FailedAccountsList = MakeListOfCollection(model.FailedAccountsCollection);
                return RedirectToAction("FailedAccounts", model);
        }

        public ActionResult FailedAccounts(ResultAccountImportWebModel model)
        {
            model.FailedAccountsCollection = MakeCollectionOfList(model.FailedAccountsList, model.Month, model.ResultYear);
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditedAccounts(ResultAccountImportWebModel model)
        {
            _resultAccountService.UpdateMenyResultAccounts(SetRealAccounts(model.FailedAccountsCollection));
            return RedirectToAction("Index", "FileImport");
        }

        private List<ResultAccountDTO> SetRealAccounts(List<ResultAccountDTO> failedAccountsCollection)
        {
            var transformedCollection = new List<ResultAccountDTO>();
            foreach(ResultAccountDTO result in failedAccountsCollection)
            {
                var tempReal = result.AccountNumber;
                result.AccountNumber = result.RealAccount;
                result.RealAccount = tempReal;
                transformedCollection.Add(result);
            }
            return transformedCollection;
        }

        private string MakeListOfCollection(IEnumerable<ResultAccountDTO> failedAccountsCollection)
        {
            var sb = new StringBuilder();
            foreach (ResultAccountDTO resultAccount in failedAccountsCollection)
            {
                sb.Append(resultAccount.AccountNumber.ToString() + ",");
            }
            sb.Remove(Convert.ToInt32(sb.ToString().Length)-1, 1);
            return sb.ToString();
        }

        private List<ResultAccountDTO> MakeCollectionOfList(string failedAccountsList, string month, string year)
        {
            var collection = new List<ResultAccountDTO>();
            var list = failedAccountsList.Split(',');
            foreach (string accountNumber in list)
            {
                collection.Add(_resultAccountService.GetOneByAccountNumberMonthAndYear(accountNumber, month, year));
            }
            return collection;
        }
    }
}