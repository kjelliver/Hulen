using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.WebCode.Attributes;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    [HulenAuthorize]
    public class ResultController : Controller
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        public ViewResult Index()
        {
            var indexModel = new ResultIndexWebModel {Results = _resultService.GetOverview()};
            return View("Index", indexModel);
        }

        public ViewResult OpenReport(int year, string period)
        {
            var indexModel = new ResultIndexWebModel { Results = _resultService.GetOverview() };
            ViewData["Message"] = "Denne funksjonen er ikke implemmentert enda.";
            return View("Index", indexModel);
        }

        public ViewResult Delete(int year, string period)
        {
            var model = new ResultDeleteWebModel();
            try
            {
                model.SelectedResult = _resultService.GetOneResultByYearAndStatus(year, period);
                return View("Delete", model);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Feil under henting av data.";
                return View("Delete", model);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Delete(ResultDeleteWebModel model)
        {
            try
            {
                _resultService.DeleteResultByYearAndStatus(model.SelectedResult.Year, model.SelectedResult.Period);
                ViewData["Message"] = "Regnskapet er slettet.";
                return View("Delete", model);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Feil under sletting av data.";
                return View("Delete", model);
            }
        }

        public ViewResult ImportResult()
        {
            var model = new ResultImportWebModel();
            model.PeriodList = new List<string> { "Januar", "Februar", "Mars", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Desember", "Revidert" };
            return View("ImportResult", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult ImportResult(HttpPostedFileBase uploadFile, ResultImportWebModel model)
        {
            if (uploadFile.ContentLength > 0)
            {
                model.FailedAccounts = _resultService.TryToImportFile(uploadFile.InputStream, model.Period, model.Year.ToString(), model.Comment).ToList();
            }

            if(model.FailedAccounts.Any())
            {
                return View("FailedAccounts", model);
            }

            model.PeriodList = new List<string> { "Januar", "Februar", "Mars", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Desember", "Revidert" };
            ViewData["Message"] = "Regnskapet er importert.";
            return View("ImportResult", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult FailedAccounts(ResultImportWebModel model)
        {
            _resultService.UpdateMenyResultAccounts(SetRealAccounts(model.FailedAccounts));
            return View("FailedAccounts", model);
        }

        private List<ResultAccountDTO> SetRealAccounts(List<ResultAccountDTO> failedAccountsCollection)
        {
            var transformedCollection = new List<ResultAccountDTO>();
            foreach (ResultAccountDTO result in failedAccountsCollection)
            {
                var tempReal = result.AccountNumber;
                result.AccountNumber = result.RealAccount;
                result.RealAccount = tempReal;
                transformedCollection.Add(result);
            }
            return transformedCollection;
        }
    }
}