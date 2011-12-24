using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Hulen.BusinessServices.Interfaces;
using Hulen.WebCode.Attributes;
using Hulen.WebCode.ViewModels;

namespace Hulen.WebCode.Controllers
{
    public class FixedArrangementCostsController : Controller
    {
        private readonly IFixedArrangementCostsService _services;

        public FixedArrangementCostsController(IFixedArrangementCostsService services)
        {
            _services = services;
        }

        [HulenAuthorize("ADMIN_ECO")]
        [HulenHandlesErrors]
        public ViewResult Index()
        {
            var model = new FixedArrangementCostsViewModel
                            {
                                FixedArrangementCosts = _services.GetFixedArrangementCosts()
                            };
            return View("Index", model);
        }

        [HulenAuthorize("ADMIN_ECO")]
        [HulenHandlesErrors]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Index(FixedArrangementCostsViewModel model)
        {
            _services.UpdateFixedArrangementCosts(model.FixedArrangementCosts);
            return View("Index", model);
        }
    }
}
