using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    public class LogInController : Controller
    {
        private readonly IUserService _userService;

        public LogInController(IUserService userService)
        {
            _userService = userService;
        }

        public ViewResult LogIn()
        {
            return View("LogIn");
        }

        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            if(_userService.ValidateUserPassword(model.UserName, model.Password))
            {
                if (HttpContext.Session != null) HttpContext.Session["currentUserID"] = model.UserName;
                return RedirectToAction("Index", "Home");
            }
            TempData["Message"] = "Feil i brukernavn eller passord";
            return RedirectToAction("LogIn", "LogIn");
        }
    }
}
