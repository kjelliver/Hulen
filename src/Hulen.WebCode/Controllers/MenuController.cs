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
        private readonly IWebService _menuService;
        private readonly IUserService _userService;

        public MenuController(IWebService menuService, IUserService userService)
        {
            _menuService = menuService;
            _userService = userService;
        }

        public ActionResult Menu()
        {
            var user = _userService.GetOneUser(Session["currentUserID"].ToString());
            var model = new MenuWebModel
                            {
                                MenuItems = _menuService.GetMenuItemsForUser(user)
                            };
            return View("Menu", model);
        }
    }
}
