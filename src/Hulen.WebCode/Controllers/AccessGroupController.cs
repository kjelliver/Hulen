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
            var model = new AccessGroupEditModel {RegisteredRoles = new List<string>()};
            try
            {
                model.AvailableRoles = _accessGroupService.GetAllRoles();
                return View("Create", model);
            }
            catch (Exception)
            {
                model.AvailableRoles = new List<string>();
                ViewData["Message"] = "Feil under henting av roller.";
                return View("Create", model);
            }
            
        }
    }
}
