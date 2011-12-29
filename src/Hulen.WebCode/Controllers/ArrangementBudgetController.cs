using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Hulen.WebCode.MvcBase;
using Hulen.WebCode.ViewModels;

namespace Hulen.WebCode.Controllers
{
    public class ArrangementBudgetController : HulenController
    {
        public ViewResult Index()
        {
            var model = new ArrangementBudgetViewModel();
            return View("Index", model);
        }
    }
}
