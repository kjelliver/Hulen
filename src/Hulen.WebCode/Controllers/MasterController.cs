using System;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    public class MasterController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMenuService _menuService;

        public MasterController(IMenuService menuService, IUserService userService)
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

        public ActionResult Banner()
        {
            try
            {
                var model = new BannerWebModel
                {
                    LoggedOnUser = _userService.GetOneUser(Session["currentUserID"].ToString())
                };
                return View("Banner", model);
            }
            catch (Exception)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }
    }
}
