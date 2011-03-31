using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;
using Hulen.Web.Mappers;
using Hulen.Web.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Hulen.Web.Controllers
{
    public class AccountInfoController : Controller
    {
        private readonly IAccountInfoRepository _repository = new AccountInfoRepository();
        private readonly AccountInfoModelMapper _mapper = new AccountInfoModelMapper();

        public ActionResult Index()
        {
            return View(_mapper.MapMenyForView(_repository.GetAllAccountCategories()).ToList());
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
                _repository.Add(_mapper.MapOneForDataBase(accountInfoModel));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            AccountInfoModel model = _mapper.MapOneForView(_repository.GetById(id));

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
                _repository.Update(_mapper.MapOneForDataBase(accountInfoModel));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
 
        public ActionResult Delete(Guid id)
        {
            return View(_mapper.MapOneForView(_repository.GetById(id)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(AccountInfoModel accountInfo)
        {
            try
            {
               _repository.Delete(_mapper.MapOneForDataBase(accountInfo));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //public FileStreamResult OpenReportInPdf()
        //{
        //    Stream filestream = _reportService.GeneratePDF("AccountInfo");

        //    HttpContext.Response.AddHeader("content-disposition", "attachment; filename=form.pdf");

        //    return new FileStreamResult(filestream, "application/pdf");
        //}
    }
}
