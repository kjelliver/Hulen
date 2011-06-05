using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    public class MenuController : Controller
    {
        private IWebService _menuService;

        public MenuController(IWebService menuService)
        {
            _menuService = menuService;
        }

        public ActionResult Menu()
        {
            var model = new MenuWebModel();
            model.MenuItems = _menuService.GetAllMenuItems();
            return View("Menu", model);
        }
    }
}
