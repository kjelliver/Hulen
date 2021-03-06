﻿using System.Web.Mvc;
using Hulen.WebCode.Attributes;

namespace Hulen.WebCode.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        [HulenAuthorize("PAGE_HOME")]
        public ActionResult Index()
        {
            ViewData["Message"] = "Velkommen til et nettsted av og for hulenpeeps";

            return View();
        }
    }
}
