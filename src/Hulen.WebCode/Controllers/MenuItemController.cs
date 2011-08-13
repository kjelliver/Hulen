using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.ViewModels;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly IWebService _webService;
        private readonly IAccessGroupService _accessGroupService;

        public MenuItemController(IWebService webService, IAccessGroupService accessGroupService)
        {
            _webService = webService;
            _accessGroupService = accessGroupService;
        }

        public ViewResult Index()
        {
            var model = new MenuItemWebModel();
            try
            {
                model.AllMenuItems = _webService.GetAllMenuItems();
                return View("Index", model);
            }
            catch (Exception)
            {
                model.AllMenuItems = new List<MenuItemDTO>();
                ViewData["Message"] = "Feil under henting av menyelementer.";
                return View("Index", model);
            }
        }

        public ViewResult Create()
        {
            var model = new MenuItemWebModel {MenuLevels = new List<int> {1, 2},};
            model.AccessGroups = GetAccessGroups();
            return View("Create", model);
        }

        private List<string> GetAccessGroups()
        {
            var names = new List<string>();
            var accessGroups = _accessGroupService.GetAllAccessGroups();

            foreach (var group in accessGroups)
            {
                names.Add(group.Name);
            }

            return names;
        }
    }
}
