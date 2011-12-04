using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.Enum;
using Hulen.Objects.Models;
using Hulen.WebCode.Attributes;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    public class AccessGroupController : Controller
    {
        private readonly IAccessGroupService _accessGroupService;
        private readonly IRoleService _roleService;

        public AccessGroupController(IAccessGroupService srv, IRoleService roleService)
        {
            _accessGroupService = srv;
            _roleService = roleService;
        }

        [HulenAuthorize("ADMIN")]
        public ViewResult Index()
        {
            var model = new AccessGroupIndexModel();
            try
            {
                model.AllAccessGroups = _accessGroupService.GetAllAccessGroups();
                return View("Index", model);
            }
            catch (Exception)
            {
                model.AllAccessGroups = new List<AccessGroup>();
                ViewData["Message"] = "Feil under henting av tilgangsgrupper.";
                return View("Index", model);
            }
        }

        [HulenAuthorize("ADMIN")]
        public ViewResult Create()
        {
            var model = new AccessGroupEditModel {RequestedRoles = new List<string>()};
            try
            {
                model.AvailableRoles = _roleService.GetAllRoles().ToList();
                return View("Create", model);
            }
            catch (Exception)
            {
                model.AvailableRoles = new List<string>();
                ViewData["Message"] = "Feil under henting av roller.";
                return View("Create", model);
            }
        }

        [HulenAuthorize("ADMIN")]
        [AcceptVerbsAttribute(HttpVerbs.Post)]
        public ViewResult Create(AccessGroupEditModel editModel, string add, string remove, string save)
        {
            ModelState.Clear();
            RestoreSavedState(editModel);
            if (!string.IsNullOrEmpty(add))
                AddRoles(editModel);
            else if (!string.IsNullOrEmpty(remove))
                RemoveRoles(editModel);
            else if (!string.IsNullOrEmpty(save))
            {
                return SaveAndReturnView(editModel, "Create");
            }
            SaveState(editModel);
            return View("Create", editModel);
        }

        [HulenAuthorize("ADMIN")]
        public ViewResult Edit(Guid id)
        {
            var model = new AccessGroupEditModel();
            try
            {
                model.AccessGroup = _accessGroupService.GetOneAccessGroup(id);
                model.RequestedRoles = model.AccessGroup.RolesThatHaveAccess;
                model.AvailableRoles = _roleService.GetAllRoles().Except(model.AccessGroup.RolesThatHaveAccess).ToList();
                model.GetSavedRoles();
                return View("Edit", model);
            }
            catch (Exception)
            {
                model.AvailableRoles = new List<string>();
                ViewData["Message"] = "Feil under henting av data.";
                return View("Edit", model);
            }
        }

        [HulenAuthorize("ADMIN")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Edit(AccessGroupEditModel editModel, string add, string remove, string save)
        {
            ModelState.Clear();
            RestoreSavedState(editModel);
            if (!string.IsNullOrEmpty(add))
                AddRoles(editModel);
            else if (!string.IsNullOrEmpty(remove))
                RemoveRoles(editModel);
            else if (!string.IsNullOrEmpty(save))
            {
                return SaveAndReturnView(editModel, "Edit");
            }
            SaveState(editModel);
            return View("Edit", editModel); 
        }

        [HulenAuthorize("ADMIN")]
        public ViewResult Delete(Guid id)
        {
            var model = new AccessGroupEditModel();
            try
            {
                model.AccessGroup = _accessGroupService.GetOneAccessGroup(id);
                model.RequestedRoles = model.AccessGroup.RolesThatHaveAccess;
                model.AvailableRoles = _roleService.GetAllRoles().Except(model.AccessGroup.RolesThatHaveAccess).ToList();
                model.GetSavedRoles();
                return View("Delete", model);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Feil under henting av data.";
                return View("Delete", model);
            }
        }

        [HulenAuthorize("ADMIN")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Delete(AccessGroupEditModel editModel)
        {
            try
            {
                ModelState.Clear();
                RestoreSavedState(editModel);
                editModel.AddRolesToAccessGroup();
                var result = _accessGroupService.DeleteOneAccessGroup(editModel.AccessGroup);

                editModel.AvailableRoles = _roleService.GetAllRoles().Except(editModel.AccessGroup.RolesThatHaveAccess).ToList();

                if (result == StorageResult.Success)
                {
                    ViewData["Message"] = "Tilgangsgruppen er slettet";
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

        private ViewResult SaveAndReturnView(AccessGroupEditModel editModel, string context)
        {
            if (!ModelState.IsValid)
                return View(context, editModel);
            try
            {
                editModel.AddRolesToAccessGroup();

                StorageResult result = context == "Create" ? _accessGroupService.SaveOneAccessGroup(editModel.AccessGroup) : _accessGroupService.UpdateOneAccessGroup(editModel.AccessGroup);

                editModel.AvailableRoles = _roleService.GetAllRoles().Except(editModel.RequestedRoles).ToList();

                switch (result)
                {
                    case StorageResult.Success:
                        ViewData["Message"] = "Tilgangsgruppen er lagret";
                        break;
                    case StorageResult.AllreadyExsists:
                        ViewData["Message"] = "Tilgangsgruppe med samme navn finnes fra før.";
                        break;
                    case StorageResult.Failed:
                        ViewData["Message"] = "Ukjent feil under lagring.";
                        break;
                }
                return View(context, editModel);
            }
            catch
            {
                ViewData["Message"] = "Feil i underliggende tjenester under lagring.";
                return View(context, editModel);
            }
        }

        private void SaveState(AccessGroupEditModel model)
        {
            model.SavedRequested = string.Join(",", model.RequestedRoles.Select(x => x.ToString()).ToArray());
            model.AvailableRoles = _roleService.GetAllRoles().Except(model.RequestedRoles).ToList();
        }

        private void RestoreSavedState(AccessGroupEditModel model)
        {
            model.RequestedRoles = new List<string>();

            if (!string.IsNullOrEmpty(model.SavedRequested))
            {
                string[] prodids = model.SavedRequested.Split(',');
                var prods = _roleService.GetAllRoles().Where(p => prodids.Contains(p.ToString()));
                model.RequestedRoles.AddRange(prods);
            }
        }

        private void AddRoles(AccessGroupEditModel model)
        {

            if (model.AvailableSelected != null)
            {
                var roles = _roleService.GetAllRoles().Where(x => model.AvailableSelected.Contains(x));
                model.RequestedRoles.AddRange(roles);
                model.AvailableSelected = null;
            }
        }

        private static void RemoveRoles(AccessGroupEditModel model)
        {
            if (model.RequestedSelected != null)
            {
                model.RequestedRoles.RemoveAll(x => model.RequestedSelected.Contains(x));
                model.RequestedSelected = null;
            }
        }
    }
}
