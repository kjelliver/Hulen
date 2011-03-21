using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;
using Hulen.Web.Models;
using Hulen.Web.Models.AccountInfo;

namespace Hulen.Web.Controllers
{
    public class AccountInfoController : Controller
    {
        private readonly IAccountInfoRepository _accountInfoRepository = new AccountInfoRepository(); 

        public ActionResult Index()
        {
            var model = new AccountInfoModels();
            model.AccountInfos = _accountInfoRepository.GetAllAccountCategories();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] AccountInfoDTO accountInfoToCreate)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                _accountInfoRepository.Add(accountInfoToCreate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        } 
 
        public ActionResult Edit(Guid id)
        {
            var accountInfoToEdit = _accountInfoRepository.GetById(id); 
            return View(accountInfoToEdit);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(AccountInfoDTO accountToEdit)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _accountInfoRepository.Update(accountToEdit);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
 
        public ActionResult Delete(Guid id)
        {
            var accountInfoToDelete = _accountInfoRepository.GetById(id);
            return View(accountInfoToDelete);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(AccountInfoDTO accountInfo)
        {
            try
            {
                _accountInfoRepository.Delete(accountInfo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
