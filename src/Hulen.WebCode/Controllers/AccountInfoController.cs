using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    public class AccountInfoController : Controller
    {
        private readonly IAccountInfoService _accountInfoService;
        private readonly IDropDownService _dropDownService; 

        public AccountInfoController()
        {
            _accountInfoService = new AccountInfoService();
            _dropDownService = new DropDownService(); 
        }

        public ActionResult Index()
        {
            var model = new AccountInfoWebModel
                            {
                                AccountInfos = _accountInfoService.GetAllAccountInfos()
                            };
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new AccountInfoWebModel();
            model.ResultCategories = _dropDownService.GetDropDownStrings("RESULT");
            model.PartsCategories = _dropDownService.GetDropDownStrings("PARTS");
            model.WeekCategories = _dropDownService.GetDropDownStrings("WEEK");
            model.IsIncomes = new List<string> {"Inntekt", "Utgift"};
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] AccountInfoWebModel accountInfoWebModel)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _accountInfoService.SaveOneAccountInfo(accountInfoWebModel.AccountInfo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            
            AccountInfoWebModel model = new AccountInfoWebModel();
            model.AccountInfo = _accountInfoService.GetOneAccountInfoById(id);
            model.ResultCategories = _dropDownService.GetDropDownStrings("RESULT");
            model.PartsCategories = _dropDownService.GetDropDownStrings("PARTS");
            model.WeekCategories = _dropDownService.GetDropDownStrings("WEEK");
            model.IsIncomes = new List<string> { "Inntekt", "Utgift" };

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(AccountInfoWebModel accountInfoWebWebModel)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _accountInfoService.UpdateOneAccountInfo(accountInfoWebWebModel.AccountInfo); 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
 
        public ActionResult Delete(Guid id)
        {
            try
            {
                _accountInfoService.DeleteOneAccountInfoById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(AccountInfoWebModel accountInfoWeb)
        {
            try
            {
                _accountInfoService.DeleteOneAccountInfo(accountInfoWeb.AccountInfo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult OpenReport()
        { 
            return RedirectToAction("AccountInfo", "Report", new { year = 2011});
        }
    }
}
