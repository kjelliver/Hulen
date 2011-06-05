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
            var model = new UserWebModel {User = _userService.GetOneUser(username)};
            return View("Edit", model);
        }
    }
}
