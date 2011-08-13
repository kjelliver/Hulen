using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Objects.ViewModels;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly IWebService _webService;
        private readonly IAccessGroupService _accessGroupService;
        private readonly IMenuService _menuService;

        public MenuItemController(IWebService webService, IAccessGroupService accessGroupService, IMenuService menuService)
        {
            _webService = webService;
            _menuService = menuService;
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
            var model = new MenuItemWebModel {MenuLevels = new List<int> {1, 2}, AccessGroups = GetAccessGroups(),};
            return View("Create", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Create(MenuItemWebModel model)
        {
            return SaveAndReturnView(model, "Create");
        }

        public ViewResult Edit(Guid id)
        {
            try
            {
                var model = new MenuItemWebModel { MenuItem = _menuService.GetOneById(id), MenuLevels = new List<int> { 1, 2 }, AccessGroups = GetAccessGroups() };
                return View("Edit", model);
            }
            catch
            {
                ViewData["Message"] = "Feil i underliggende tjenester under henting av menyelement.";
                return View("Index");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Edit(MenuItemWebModel model)
        {
            return SaveAndReturnView(model, "Edit");
        }

        private ViewResult SaveAndReturnView(MenuItemWebModel model, string context)
        {
            if (!ModelState.IsValid)
            {
                return View(context, model);
            }

            try
            {
                model.MenuLevels = new List<int> { 1, 2 };
                model.AccessGroups = GetAccessGroups();

                var storageresult = context == "Create" ? _menuService.SaveOneMenuItem(model.MenuItem) : _menuService.UpdateOne(model.MenuItem);

                switch (storageresult)
                {
                    case StorageResult.Success:
                        ViewData["Message"] = "Menyelementet er lagret";
                        break;
                    case StorageResult.AllreadyExsists:
                        ViewData["Message"] = "Menyelementet med samme navn finnes fra før.";
                        break;
                    case StorageResult.Failed:
                        ViewData["Message"] = "Ukjent feil under lagring.";
                        break;
                }
                return View(context, model);
            }
            catch
            {
                ViewData["Message"] = "Feil i underliggende tjenester under lagring.";
                return View(context, model);
            }
        }

        public ViewResult Delete(Guid id)
        {
            try
            {
                var model = new MenuItemWebModel { MenuItem = _menuService.GetOneById(id), MenuLevels = new List<int> { 1, 2 }, AccessGroups = GetAccessGroups() };
                return View("Delete", model);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Feil under henting av data.";
                return View("Index");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Delete(MenuItemWebModel editModel)
        {
            try
            {
                var result = _menuService.DeleteOneMenuItem(editModel.MenuItem );

                if (result == StorageResult.Success)
                {
                    ViewData["Message"] = "Menyelementet er slettet";
                    return View("Delete", editModel);
                }
                ViewData["Message"] = "Ukjent feil under sletting.";
                return View("Delete", editModel);
            }
            catch
            {
                ViewData["Message"] = "Feil i underliggende tjenester under sletting.";
                return View("Delete", editModel);
            }
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
