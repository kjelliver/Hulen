using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.Web.Mappers;
using Hulen.Web.Models;

namespace Hulen.Web.Controllers
{
    public class AccountInfoController : Controller
    {
        private readonly IAccountInfoServices _accountInfoService = new AccountInfoServices();
        private readonly AccountInfoModelMapper _mapper = new AccountInfoModelMapper();

        public ActionResult Index()
        {
            return View(_mapper.MapMenyForView(_accountInfoService.GetAllAccounts()).ToList());
        }

        public ActionResult Create()
        {
            var model = new AccountInfoModel();
            model.ResultCategories = new List<string> {"Udefinert"};
            model.PartsCategories = new List<string> { "Udefinert", "Bar", "Arrangement", "Personalkostnader", "PR", "Støtte og tilskudd", "Økonomi", "Driftskostnader" };
            model.WeekCategories = new List<string> { "Udefinert" };
            model.IsIncomes = new List<string> {"Inntekt", "Utgift"};
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] AccountInfoModel accountInfoModel)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _accountInfoService.StoreNewAccountInfo(_mapper.MapOneForDataBase(accountInfoModel));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            AccountInfoModel model = _mapper.MapOneForView(_accountInfoService.GetAccountById(id));

            model.ResultCategories = new List<string> { "Udefinert" };
            model.PartsCategories = new List<string> { "Udefinert", "Bar", "Arrangement", "Personalkostnader", "PR", "Støtte og tilskudd", "Økonomi", "Driftskostnader" };
            model.WeekCategories = new List<string> { "Udefinert" };
            model.IsIncomes = new List<string> { "Inntekt", "Utgift" };

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(AccountInfoModel accountInfoModel)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _accountInfoService.UpdateAccountInfo(_mapper.MapOneForDataBase(accountInfoModel));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
 
        public ActionResult Delete(Guid id)
        {
            return View(_mapper.MapOneForView(_accountInfoService.GetAccountById(id)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(AccountInfoModel accountInfo)
        {
            try
            {
                _accountInfoService.Delete(_mapper.MapOneForDataBase(accountInfo));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult OpenReportInExcel()
        {
            _accountInfoService.GeneratePdf();
            //HttpContext.Response.AddHeader( )

            return RedirectToAction("Index");
        }
    }
}
