using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.Enum;
using Hulen.Objects.ViewModels;
using Hulen.WebCode.Attributes;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    [HulenAuthorize]
    public class AccessGroupController : Controller
    {
        private readonly IAccessGroupService _accessGroupService;

        public AccessGroupController(IAccessGroupService srv)
        {
            _accessGroupService = srv;
        }

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
                model.AllAccessGroups = new List<AccessGroupViewModel>();
                ViewData["Message"] = "Feil under henting av tilgangsgrupper.";
                return View("Index", model);
            }
        }

        public ViewResult Create()
        {
            var model = new AccessGroupEditModel {RequestedRoles = new List<string>()};
            try
            {
                model.AvailableRoles = _accessGroupService.GetAllRoles().ToList();
                return View("Create", model);
            }
            catch (Exception)
            {
                model.AvailableRoles = new List<string>();
                ViewData["Message"] = "Feil under henting av roller.";
                return View("Create", model);
            }
        }

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
                return SaveAccessGroupAndReturnView(editModel);
            }
            SaveState(editModel);
            return View("Create", editModel);
        }

        public ViewResult Edit(Guid id)
        {
            var model = new AccessGroupEditModel();
            try
            {
                model.AccessGroup = _accessGroupService.GetOneAccessGroup(id);
                model.RequestedRoles = model.AccessGroup.RolesThatHaveAccess;
                model.AvailableRoles = _accessGroupService.GetAllRoles().Except(model.AccessGroup.RolesThatHaveAccess).ToList();
                return View("Edit", model);
            }
            catch (Exception)
            {
                model.AvailableRoles = new List<string>();
                ViewData["Message"] = "Feil under henting av data.";
                return View("Edit", model);
            }
        }

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
                return UpdateAccessGroupAndReturnView(editModel);
            }
            SaveState(editModel);
            return View("Edit", editModel); 
        }

        public ViewResult Delete(Guid id)
        {
            var model = new AccessGroupEditModel();
            try
            {
                model.AccessGroup = _accessGroupService.GetOneAccessGroup(id);
                return View("Delete", model);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Feil under henting av data.";
                return View("Delete", model);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Delete(AccessGroupEditModel editModel)
        {
            try
            {
                var result = _accessGroupService.DeleteOneAccessGroup(editModel.AccessGroup);
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

        private ViewResult SaveAccessGroupAndReturnView(AccessGroupEditModel editModel)
        {
            if (!ModelState.IsValid)
                return View("Create", editModel);
            try
            {
                if (editModel.AccessGroup.RolesThatHaveAccess == null)
                    editModel.AccessGroup.RolesThatHaveAccess = new List<string>();
                editModel.AccessGroup.RolesThatHaveAccess.AddRange(editModel.RequestedRoles);

                var result = _accessGroupService.SaveOneAccessGroup(editModel.AccessGroup);

                editModel.AvailableRoles = _accessGroupService.GetAllRoles().Except(editModel.RequestedRoles).ToList();

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
                return View("Create", editModel);
            }
            catch
            {
                ViewData["Message"] = "Feil i underliggende tjenester under lagring.";
                return View("Create", editModel);
            }
        }

        private ViewResult UpdateAccessGroupAndReturnView(AccessGroupEditModel editModel)
        {
            if (!ModelState.IsValid)
                return View("Edit", editModel);
            try
            {
                if (editModel.AccessGroup.RolesThatHaveAccess == null)
                    editModel.AccessGroup.RolesThatHaveAccess = new List<string>();
                editModel.AccessGroup.RolesThatHaveAccess.AddRange(editModel.RequestedRoles);

                var result = _accessGroupService.UpdateOneAccessGroup(editModel.AccessGroup);

                editModel.AvailableRoles = _accessGroupService.GetAllRoles().Except(editModel.RequestedRoles).ToList();

                if (result == StorageResult.Success)
                {
                    ViewData["Message"] = "Tilgangsgruppen er endret";
                    return View("Edit", editModel);
                }
                if (result == StorageResult.AllreadyExsists)
                {
                    ViewData["Message"] = "Tilgangsgruppe med samme navn finnes fra før.";
                    return View("Edit", editModel);
                }
                ViewData["Message"] = "Ukjent feil under lagring.";
                return View("Edit", editModel);
            }
            catch
            {
                ViewData["Message"] = "Feil i underliggende tjenester under lagring.";
                return View("Edit", editModel);
            }
        }

        private void SaveState(AccessGroupEditModel model)
        {
            model.SavedRequested = string.Join(",", model.RequestedRoles.Select(x => x.ToString()).ToArray());
            model.AvailableRoles = _accessGroupService.GetAllRoles().Except(model.RequestedRoles).ToList();
        }

        private void RestoreSavedState(AccessGroupEditModel model)
        {
            model.RequestedRoles = new List<string>();

            if (!string.IsNullOrEmpty(model.SavedRequested))
            {
                string[] prodids = model.SavedRequested.Split(',');
                var prods = _accessGroupService.GetAllRoles().Where(p => prodids.Contains(p.ToString()));
                model.RequestedRoles.AddRange(prods);
            }
        }

        private void AddRoles(AccessGroupEditModel model)
        {

            if (model.AvailableSelected != null)
            {
                var roles = _accessGroupService.GetAllRoles().Where(x => model.AvailableSelected.Contains(x));
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
