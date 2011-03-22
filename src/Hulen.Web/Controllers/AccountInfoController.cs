using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.BusinessServices.ViewModels;

namespace Hulen.Web.Controllers
{
    public class AccountInfoController : Controller
    {
        private readonly IAccountInfoServices _accountInfoService = new AccountInfoServices();

        public ActionResult Index()
        {
            return View(_accountInfoService.GetAllAccounts().ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] AccountInfoViewModel accountInfoModelView)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _accountInfoService.StoreNewAccountInfo(accountInfoModelView);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {       
            return View(_accountInfoService.GetAccountById(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(AccountInfoViewModel accountInfoViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _accountInfoService.UpdateAccountInfo(accountInfoViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
 
        public ActionResult Delete(Guid id)
        {
            return View(_accountInfoService.GetAccountById(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(AccountInfoViewModel accountInfo)
        {
            try
            {
                _accountInfoService.Delete(accountInfo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
