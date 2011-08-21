using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.ViewModels;
using Hulen.WebCode.Attributes;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    [HulenAuthorize]
    public class BudgetController : Controller
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        public ViewResult Index()
        {
            var indexModel = new BudgetIndexWebModel();
            try
            {
                indexModel.StoredBudgets = _budgetService.GetOverviewAllStoredBudgets();
                return View("Index", indexModel);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Feil under henting av budsjetter.";
                indexModel.StoredBudgets = new List<BudgetOverviewViewModel>();
                return View("Index", indexModel);
            }

        }
    }
}
