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
            try
            {
                if (_userService.ValidateUserPassword(model.UserName, model.Password))
                {
                    if (_userService.GetOneUser(model.UserName).MustChangePassword)
                        return RedirectToAction("ChangePassword", "LogIn");
                    if (HttpContext.Session != null) HttpContext.Session["currentUserID"] = model.UserName;
                    return RedirectToAction("Index", "Home");
                }
                TempData["Message"] = "Feil i brukernavn eller passord";
                return RedirectToAction("LogIn", "LogIn");
            }
            catch (Exception)
            {
                TempData["Message"] = "Ukjent feil har oppstått";
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        public ViewResult ChangePassword()
        {
            var model = new NewPasswordModel();
            return View("ChangePassword", model);
        }

        [HttpPost]
        public ActionResult ChangePassword(NewPasswordModel model)
        {
            try
            {
                if(model.NewPassword == model.RepeatPassword)
                {
                    _userService.UpdatePassword(model.UserName, model.NewPassword);
                    return RedirectToAction("Index", "Home");
                }
                TempData["Message"] = "Nytt passord og gjentatt passord er ikke likt.";
                return View("ChangePassword", model);
            }
            catch (Exception)
            {
                TempData["Message"] = "Ukjent feil har oppstått";
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        public ActionResult LogOut()
        {
            if (HttpContext.Session != null) HttpContext.Session["currentUserID"] = "";
            return RedirectToAction("LogIn", "LogIn");
        }
    }
}
