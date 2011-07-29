using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.ViewModels;
using Hulen.WebCode.Attributes;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    //[HulenAuthorize]
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
                AddProducts(editModel);
            else if (!string.IsNullOrEmpty(remove))
                RemoveProducts(editModel);
            else if (!string.IsNullOrEmpty(save))
            {
                return View("Create", editModel);
                //todo: implement SendListToSanta method...
            }
            SaveState(editModel);
            return View("Create", editModel);
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
                string[] savedRoles = model.SavedRequested.Split(',');
                var roles = _accessGroupService.GetAllRoles().Where(x => x == savedRoles.ToString());
                model.RequestedRoles.AddRange(roles);
            }
        }

        private void AddProducts(AccessGroupEditModel model)
        {
            if (model.AvailableSelected != null)
            {
                var roles = _accessGroupService.GetAllRoles().Where(x => model.AvailableSelected.Contains(x));
                model.RequestedRoles.AddRange(roles);
                model.AvailableSelected = null;
            }
        }

        private static void RemoveProducts(AccessGroupEditModel model)
        {
            if (model.RequestedSelected != null)
            {
                model.RequestedRoles.RemoveAll(x => model.RequestedSelected.Contains(x));
                model.RequestedSelected = null;
            }
        }
    }
}
