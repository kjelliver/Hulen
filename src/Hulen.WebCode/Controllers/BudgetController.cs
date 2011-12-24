using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.WebCode.Attributes;
using Hulen.WebCode.ViewModels;

namespace Hulen.WebCode.Controllers
{
    public class BudgetController : Controller
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HulenAuthorize("PAGE_BUDGET")]
        public ViewResult Index()
        {
            var indexModel = new BudgetIndexWebModel();
            try
            {
                indexModel.StoredBudgets = _budgetService.GetOverview();
                return View("Index", indexModel);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Feil under henting av budsjetter.";
                indexModel.StoredBudgets = new List<Budget>();
                return View("Index", indexModel);
            }
        }

        [HulenAuthorize("PAGE_RESULT")]
        public ViewResult OpenReport(int year, string budgetStatus)
        {
            var indexModel = new BudgetIndexWebModel { StoredBudgets = _budgetService.GetOverview() };
            ViewData["Message"] = "Denne funksjonen er ikke implemmentert enda.";
            return View("Index", indexModel);
        }

        [HulenAuthorize("FEATURE_BUDGET_EDIT")]
        public ViewResult Delete(int year, string budgetStatus)
        {
            var model = new BudgetDeleteWebModel();
            try
            {
                model.SelectedBudget = _budgetService.GetOneBudgetByYearAndStatus(year, budgetStatus);
                return View("Delete", model);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Feil under henting av data.";
                return View("Delete", model);
            }
        }

        [HulenAuthorize("FEATURE_BUDGET_EDIT")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Delete(BudgetDeleteWebModel model)
        {
            try
            {
                _budgetService.DeleteAllBudgetsByYearAndStatus(Convert.ToInt32(model.SelectedBudget.Year), model.SelectedBudget.BudgetStatus );
                ViewData["Message"] = "Budsjettet er slettet.";
                return View("Delete", model);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Feil under sletting av data.";
                return View("Delete", model);
            }
        }

        [HulenAuthorize("FEATURE_BUDGET_EDIT")]
        public ViewResult ImportBudget()
        {
            var model = new BudgetImportWebModel();
            model.BudgetStatusList = new List<string> { "Orginalt", "Revidert" };
            return View("ImportBudget", model);
        }

        [HulenAuthorize("FEATURE_BUDGET_EDIT")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult ImportBudget(HttpPostedFileBase uploadFile, BudgetImportWebModel model)
        {
            if (uploadFile.ContentLength > 0)
            {
                _budgetService.ImportFile(uploadFile.InputStream, model.BudgetYear, model.BudgetStatus, model.Comment);
            }

            model.BudgetStatusList = new List<string> { "Orginalt", "Revidert" };
            ViewData["Message"] = "Budsjettet er importert.";
            return View("ImportBudget", model);
        }
    }
}
