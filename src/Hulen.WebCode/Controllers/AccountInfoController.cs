using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.WebCode.Attributes;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    [HulenAuthorize]
    public class AccountInfoController : Controller
    {
        private readonly IAccountInfoService _accountInfoService;
        private readonly IDropDownService _dropDownService;

        public AccountInfoController(IAccountInfoService accountInfoService)
        {
            _accountInfoService = accountInfoService;
        }

        public AccountInfoController(IAccountInfoService accountInfoService, IDropDownService dropDownService)
        {
            _accountInfoService = accountInfoService;
            _dropDownService = dropDownService; 
        }

        public ViewResult Index()
        {
            try
            {
                var model = new AccountInfoWebModel
                {
                    AccountInfos = _accountInfoService.GetAllAccountInfosByYear(DateTime.Now.Year)
                };
                if (model.AccountInfos.Any())
                {
                    return View("Index", model);
                }
                ViewData["Message"] = "Ingen kontoer funnet for gitt år.";
                return View("Index");
            }
            catch (Exception)
            {
                ViewData["Message"] = "En feil oppstod, vennligst prøv på nytt.";
                return View("Index");
            }
        }

    //    public ActionResult Create()
    //    {
    //        var model = new AccountInfoWebModel();
    //        model.ResultCategories = _dropDownService.GetDropDownStrings("RESULT");
    //        model.PartsCategories = _dropDownService.GetDropDownStrings("PARTS");
    //        model.WeekCategories = _dropDownService.GetDropDownStrings("WEEK");
    //        model.IsIncomes = new List<string> {"Inntekt", "Utgift"};
    //        return View(model);
    //    }

    //    [AcceptVerbs(HttpVerbs.Post)]
    //    public ActionResult Create([Bind(Exclude = "Id")] AccountInfoWebModel accountInfoWebModel)
    //    {
    //        if (!ModelState.IsValid)
    //            return View();

    //        try
    //        {
    //            _accountInfoService.SaveOneAccountInfo(accountInfoWebModel.AccountInfo);
    //            return RedirectToAction("Index");
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    public ActionResult Edit(Guid id)
    //    {
            
    //        AccountInfoWebModel model = new AccountInfoWebModel();
    //        model.AccountInfo = _accountInfoService.GetOneAccountInfoById(id);
    //        model.ResultCategories = _dropDownService.GetDropDownStrings("RESULT");
    //        model.PartsCategories = _dropDownService.GetDropDownStrings("PARTS");
    //        model.WeekCategories = _dropDownService.GetDropDownStrings("WEEK");
    //        model.IsIncomes = new List<string> { "Inntekt", "Utgift" };

    //        return View(model);
    //    }

    //    [AcceptVerbs(HttpVerbs.Post)]
    //    public ActionResult Edit(AccountInfoWebModel accountInfoWebWebModel)
    //    {
    //        if (!ModelState.IsValid)
    //            return View();

    //        try
    //        {
    //            _accountInfoService.UpdateOneAccountInfo(accountInfoWebWebModel.AccountInfo); 
    //            return RedirectToAction("Index");
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }
 
    //    public ActionResult Delete(Guid id)
    //    {
    //        try
    //        {
    //            _accountInfoService.DeleteOneAccountInfoById(id);
    //            return RedirectToAction("Index");
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    [AcceptVerbs(HttpVerbs.Post)]
    //    public ActionResult Delete(AccountInfoWebModel accountInfoWeb)
    //    {
    //        try
    //        {
    //            _accountInfoService.DeleteOneAccountInfo(accountInfoWeb.AccountInfo);
    //            return RedirectToAction("Index");
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    public ActionResult OpenReport()
    //    { 
    //        return RedirectToAction("AccountInfo", "Report", new { year = 2011});
    //    }
    }
}
