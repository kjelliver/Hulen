using System;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.Objects.Enum;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public ViewResult Index()
        {
            var model = new UserWebModel {Users = _userService.GetAllUsers()};
            return View("Index", model);
        }

        public ViewResult Create()
        {
            var model = new UserWebModel();
            return View("Create", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Create(UserWebModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", model);
            }

            try
            {
                var storageresult = _userService.SaveOneUser(model.User);

                if (storageresult == StorageResult.Success)
                {
                    model = new UserWebModel(); 
                    ViewData["Message"] = "Brukeren er opprettet";
                    return View("Create", model);
                }
                if (storageresult == StorageResult.AllreadyExsists)
                {
                    ViewData["Message"] = "Brukeren finnes fra før.";
                    return View("Create", model);
                }
                ViewData["Message"] = "Ukjent feil under lagring.";
                return View("Create", model);
            }
            catch
            {
                ViewData["Message"] = "Feil i underliggende tjenester under lagring.";
                return View("Create", model);
            }
        }

        public ViewResult Edit(string username)
        {
            try
            {
                var model = new UserWebModel { User = _userService.GetOneUser(username) };
                model.UserNameStoredInDb = model.User.Username;
                return View("Edit", model);
            }
            catch
            {
                ViewData["Message"] = "Feil i underliggende tjenester under henting av bruker.";
                return View("Edit");
            }  
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Edit(UserWebModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            try
            {
                var result = _userService.UpdateOneUser(model.User, IsUsernameChanged(model));

                if (result == StorageResult.Success)
                {
                    model.UserNameStoredInDb = model.User.Username;
                    ViewData["Message"] = "Brukeren er endret.";
                    return View("Edit", model);
                }
                if (result == StorageResult.AllreadyExsists)
                {
                    ViewData["Message"] = "Brukernavn finnes fra før.";
                    return View("Edit", model);
                }
                ViewData["Message"] = "Ukjent feil under lagring.";
                return View("Edit", model);
            }
            catch
            {
                ViewData["Message"] = "Feil i underliggende tjenester under lagring av bruker.";
                return View("Edit", model);
            }
        }

        private static bool IsUsernameChanged(UserWebModel model)
        {
            return model.User.Username != model.UserNameStoredInDb;
        }
    }
}
