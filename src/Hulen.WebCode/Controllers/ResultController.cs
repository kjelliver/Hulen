using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.PdfGenerator;
using Hulen.WebCode.Attributes;
using Hulen.WebCode.Models;

namespace Hulen.WebCode.Controllers
{
    public class ResultController : Controller
    {
        private readonly IResultService _resultService;
        private readonly IPdfGenerator _pdfGenerator;

        public ResultController(IResultService resultService, IPdfGenerator pdfGenerator)
        {
            _resultService = resultService;
            _pdfGenerator = pdfGenerator;
        }

        [HulenAuthorize("PAGE_RESULT")]
        public ViewResult Index()
        {
            var indexModel = new ResultIndexWebModel {Results = _resultService.GetOverview()};
            return View("Index", indexModel);
        }

        [HulenAuthorize("PAGE_RESULT")]
        public ActionResult OpenReport(int year, string period, string usedBudget)
        {
            var dir = new Dictionary<string, string> {{"Year", year.ToString()}, {"Period", period}, {"UsedBudget", usedBudget}};
            var fileStream = _pdfGenerator.GetPdf("RESULT_REPORT", dir); 
            var ms = new MemoryStream(fileStream);
            return new FileStreamResult(ms, "application/pdf");
        }

        [HulenAuthorize("TEST")]
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

        [HulenAuthorize("TEST")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Delete(ResultDeleteWebModel model)
        {
            try
            {
                _resultService.DeleteResultByMonth(model.SelectedResult.Period, model.SelectedResult.Year);
                ViewData["Message"] = "Regnskapet er slettet.";
                return View("Delete", model);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Feil under sletting av data.";
                return View("Delete", model);
            }
        }

        [HulenAuthorize("TEST")]
        public ViewResult ImportResult()
        {
            var model = new ResultImportWebModel
                            {
                                PeriodList =
                                    new List<string>
                                        {
                                            "Januar",
                                            "Februar",
                                            "Mars",
                                            "April",
                                            "Mai",
                                            "Juni",
                                            "Juli",
                                            "August",
                                            "September",
                                            "Oktober",
                                            "November",
                                            "Desember",
                                            "Revidert"
                                        },
                                Year = DateTime.Now.Year, BudgetStatusList = new List<string> {"Orginalt", "Revidert"}          
                            };
            return View("ImportResult", model);
        }

        [HulenAuthorize("TEST")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult ImportResult(HttpPostedFileBase uploadFile, ResultImportWebModel model)
        {
            if (uploadFile.ContentLength > 0)
            {
                model.FailedAccounts = _resultService.TryToImportFile(uploadFile.InputStream, model.Period, model.Year.ToString(), model.Comment, model.UsedBudget).ToList();
            }

            if(model.FailedAccounts.Any())
            {
                return View("FailedAccounts", model);
            }

            model.PeriodList = new List<string> { "Januar", "Februar", "Mars", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Desember", "Revidert" };
            model.Year = DateTime.Now.Year;
            ViewData["Message"] = "Regnskapet er importert.";
            return View("ImportResult", model);
        }

        [HulenAuthorize("TEST")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult FailedAccounts(ResultImportWebModel model)
        {
            _resultService.SaveMenyResultAccounts(SetRealAccounts(model.FailedAccounts));
            ViewData["Message"] = "Endringene er lagret.";
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