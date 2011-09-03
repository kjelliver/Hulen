using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.Objects.Enum;
using Hulen.PdfGenerator;
using Hulen.WebCode.Attributes;
using Hulen.WebCode.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Hulen.WebCode.Controllers
{
    public class AccountInfoController : Controller
    {
        private readonly IAccountInfoService _accountInfoService;

        public AccountInfoController(IAccountInfoService accountInfoService)
        {
            _accountInfoService = accountInfoService;
        }

        [HulenAuthorize("PAGE_ACCOUNTINFO")]
        public ViewResult Index(string message, int year = 0)
        {
            try
            {
                var model = new AccountInfoIndexModel
                                {
                                    AccountInfos = _accountInfoService.GetAllAccountInfosByYear(year == 0 ? DateTime.Now.Year : year),
                                    DefaultYear = year == 0 ? DateTime.Now.Year.ToString() : year.ToString(),
                                    Years = GetDropDownList("YEAR")
                                };

                if (!model.AccountInfos.Any())
                {
                    ViewData["Message"] = "Ingen kontoer funnet for gitt år.";
                    return View("Index", model);
                }
                ViewData["Message"] = message;
                return View("Index", model);
            }
            catch (Exception)
            {
                var model = new AccountInfoIndexModel();
                ViewData["Message"] = "En feil oppstod, vennligst prøv på nytt.";
                return View("Index", model);
            }
        }

        [HulenAuthorize("PAGE_ACCOUNTINFO")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Index(AccountInfoIndexModel model)
        {
            var year = int.Parse(model.SelectedYear);
            return Index("", year);
        }

        [HulenAuthorize("FEAURE_ACCOUNTINFO_EDIT")]
        public ViewResult Create()
        {
            var model = new AccountInfoEditModel();
            model.FillDropDownLists();
            return View("Create", model);
        }

        [HulenAuthorize("FEAURE_ACCOUNTINFO_EDIT")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Create([Bind(Exclude = "Id")] AccountInfoEditModel model)
        {
            if (!ModelState.IsValid)
                return View("Create", model);

            try
            {
                var result = _accountInfoService.SaveOneAccountInfo(model.AccountInfo);
                if(result == StorageResult.Success)
                {
                    model.FillDropDownLists();
                    ViewData["Message"] = "Kontoinformasjonen er opprettet";
                    return View("Create", model);    
                }
                if(result == StorageResult.AllreadyExsists)
                {
                    model.FillDropDownLists();
                    ViewData["Message"] = "Kontoinformasjon med samme nummer og år finnes fra før.";
                    return View("Create", model);  
                }
                model.FillDropDownLists();
                ViewData["Message"] = "Ukjent feil under lagring.";
                return View("Create", model);
            }
            catch
            {
                model.FillDropDownLists(); 
                ViewData["Message"] = "Feil i underliggende tjenester under lagring.";
                return View("Create", model);
            }
        }

        [HulenAuthorize("FEAURE_ACCOUNTINFO_EDIT")]
        public ViewResult Edit(Guid id)
        {

            var model = new AccountInfoEditModel
                            {
                                AccountInfo = _accountInfoService.GetOneAccountInfoById(id)
                            };
            model.FillDropDownLists();
            return View(model);
        }

        [HulenAuthorize("FEAURE_ACCOUNTINFO_EDIT")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(AccountInfoEditModel accountInfoWebWebModel)
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

        [HulenAuthorize("FEAURE_ACCOUNTINFO_EDIT")]
        public ViewResult Delete(Guid id)
        {
            try
            {
                _accountInfoService.DeleteOneAccountInfoById(id);
                return Index("Kontoinformasjonen er slettet.");
            }
            catch
            {
                return Index("Feil under sletting");
            }
        }

        [HulenAuthorize("FEAURE_ACCOUNTINFO_EDIT")]
        public ViewResult Copy()
        {
            var model = new AccountInfoCopyModel();
            model.FillYears();
            return View("Copy", model);
        }

        [HulenAuthorize("FEAURE_ACCOUNTINFO_EDIT")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Copy(AccountInfoCopyModel copyModel)
        {   
            _accountInfoService.CopyAccountInfo(int.Parse(copyModel.SelectedCopyFromYear), int.Parse(copyModel.SelectedCopyToYear));
            var model = new AccountInfoCopyModel();
            model.FillYears();
            ViewData["Message"] = "Kontoinformasjonen er kopiert";
            return View("Copy", model);
        }

        [HulenAuthorize("FEAURE_ACCOUNTINFO_EDIT")]
        public ViewResult Import()
        {
            var model = new AccountInfoImportModel();
            return View(model);
        }

        [HulenAuthorize("FEAURE_ACCOUNTINFO_EDIT")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Import(HttpPostedFileBase uploadFile, AccountInfoImportModel importModel)
        {
            if (uploadFile.ContentLength > 0)
            {
                _accountInfoService.ImportFile(uploadFile.InputStream, importModel.AccountInfoYear);
            }
            ViewData["Message"] = "Filen er importert";
            return View("Import", importModel );
        }

        private static List<string> GetDropDownList(string context)
        {
            if (context == "RESULT")
                return new List<string> { "Udefinert", "Salgsinntekter", "AndreInntekter", "Varekjøp", "Personalkostnader", "Driftskostnader", "Finansielle" };
            if (context == "PARTS")
                return new List<string> { "Udefinert", "Bar", "Arrangement", "Personalkostnader", "PublicRelations", "Tilskudd", "Økonomi", "Driftskostnader" };
            if (context == "WEEK")
                return new List<string> { "Udefinert", "PublicRelations" };
            if (context == "INCOME")
                return new List<string> { "Inntekt", "Utgift" };
            if (context == "YEAR")
                return new List<string> {"2010", "2011", "2012"};
            return new List<string>();
        }
    }
}
