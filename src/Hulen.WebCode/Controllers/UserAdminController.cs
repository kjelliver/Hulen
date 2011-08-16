using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.Enum;
using Hulen.WebCode.Attributes;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    [HulenAuthorize]
    public class UserAdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserAdminController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public ViewResult Index(string message)
        {
            ViewData["Message"] = message;
            var model = new UserWebModel { Users = _userService.GetAllUsers() };
            return View("Index", model);  
        }

        public ViewResult Create()
        {
            var model = new UserWebModel {Roles = _roleService.GetAllRoles()};
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
                    model = new UserWebModel {Roles = _roleService.GetAllRoles()}; 
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
                var model = new UserWebModel { User = _userService.GetOneUser(username), Roles = _roleService.GetAllRoles()};
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
        public ViewResult Edit(UserWebModel model, string save, string reset)
        {
            if (!string.IsNullOrEmpty(save))
                return UpdateUser(model);
            return ResetPassword(model);
        }

        public ViewResult Delete(string username)
        {
            try
            {
                var deleteResult = _userService.DeleteOneUserByUserName(username);
                if(deleteResult == StorageResult.Success){
                    return Index("Brukerenkontoen til " + username + " er slettet.");
                }
                if(deleteResult == StorageResult.Failed)
                {
                    return Index("Feil i underliggende tjenester ved sletting av brukerkontoen til " + username + ".");
                }
                return Index("Ukjent feil under sletting.");
            }
            catch
            {
                return Index("Feil i underliggende tjenester ved sletting av brukerkontoen til " + username + ".");
            }

        }

        private static bool IsUsernameChanged(UserWebModel model)
        {
            return model.User.Username != model.UserNameStoredInDb;
        }

        private ViewResult UpdateUser(UserWebModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            try
            {
                model.Roles = _roleService.GetAllRoles(); 
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

        private ViewResult ResetPassword(UserWebModel model)
        {
            try
            {
                model.User.Password = "12345";
                model.User.MustChangePassword = true;
                model.Roles = _roleService.GetAllRoles(); 
                var result = _userService.UpdateOneUser(model.User, IsUsernameChanged(model));

                if (result == StorageResult.Success)
                {
                    model.UserNameStoredInDb = model.User.Username;
                    ViewData["Message"] = "Passordet er resatt.";
                    return View("Edit", model);
                }
                ViewData["Message"] = "Ukjent feil under resetting av passord.";
                return View("Edit", model);
            }
            catch
            {
                ViewData["Message"] = "Feil i underliggende tjenester under resetting av passord.";
                return View("Edit", model);
            }
        }
    }
}
