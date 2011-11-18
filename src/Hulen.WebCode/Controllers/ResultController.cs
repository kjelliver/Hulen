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
using Hulen.WebCode.MvcBase;

namespace Hulen.WebCode.Controllers
{
    public class ResultController : HulenController
    {
        private readonly IResultService _resultService;
        private readonly IPdfGenerator _pdfGenerator;
        private readonly IAccountInfoService _accountInfoService;

        public ResultController(IResultService resultService, IPdfGenerator pdfGenerator, IAccountInfoService accountInfoService)
        {
            _resultService = resultService;
            _accountInfoService = accountInfoService;
            _pdfGenerator = pdfGenerator;
        }

        [HulenAuthorize("PAGE_RESULT")]
        public ViewResult Index(string message, int year = 0)
        {
            try
            {
                var model = new ResultIndexWebModel
                                {
                                    Results = _resultService.GetOverviewByYear(year == 0 ? DateTime.Now.Year : year),
                                    DefaultYear = year == 0 ? DateTime.Now.Year.ToString() : year.ToString(),
                                    Years = GetYearsForCombobox()
                                };

                if (!model.Results.Any())
                {
                    ViewData["Message"] = "Ingen regnskapskontoer funnet for gitt år.";
                    return View("Index", model);
                }
                ViewData["Message"] = message;
                return View("Index", model);
            }
            catch (Exception)
            {
                var model = new ResultIndexWebModel();
                ViewData["Message"] = "En feil oppstod, vennligst prøv på nytt.";
                return View("Index", model);
            }
        }

        [HulenAuthorize("PAGE_RESULT")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Index(ResultIndexWebModel model)
        {
            var year = Convert.ToInt32(model.SelectedYear);
            return Index("", year);
        }

        [HulenAuthorize("PAGE_RESULT")]
        public ActionResult OpenReport(int year, string period, string usedBudget)
        {
            var dir = new Dictionary<string, string> {{"Year", year.ToString()}, {"Period", period}, {"UsedBudget", usedBudget}};
            var fileStream = _pdfGenerator.GetPdf("RESULT_REPORT", dir); 
            var ms = new MemoryStream(fileStream);
            return new FileStreamResult(ms, "application/pdf");
        }

        [HulenAuthorize("FEATURE_RESULT_EDIT")]
        public ViewResult Delete(int year, string period)
        {
            var model = new ResultDeleteWebModel();
            try
            {
                model.SelectedResult = _resultService.GetOneResultByYearAndStatus(period, year);
                return View("Delete", model);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Feil under henting av data.";
                return View("Delete", model);
            }
        }

        [HulenAuthorize("FEATURE_RESULT_EDIT")]
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

        [HulenAuthorize("FEATURE_RESULT_EDIT")]
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

        [HulenAuthorize("FEATURE_RESULT_EDIT")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult ImportResult(HttpPostedFileBase uploadFile, ResultImportWebModel model)
        {
            if (uploadFile.ContentLength > 0)
            {
                model.FailedAccounts = _resultService.TryToImportFile(uploadFile.InputStream, model.Period, model.Year.ToString(), model.Comment, model.UsedBudget).ToList();
            }

            if(model.FailedAccounts.Any())
            {
                model.Accounts = _accountInfoService.GetAllAccountInfosByYear(model.Year);
                return View("FailedAccounts", model);
            }

            model.PeriodList = new List<string> { "Januar", "Februar", "Mars", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Desember", "Revidert" };
            model.Year = DateTime.Now.Year;
            ViewData["Message"] = "Regnskapet er importert.";
            return View("ImportResult", model);
        }

        [HulenAuthorize("FEATURE_RESULT_EDIT")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult FailedAccounts(ResultImportWebModel model)
        {
            try
            {
                _resultService.SaveMenyResultAccounts(SetRealAccounts(model.FailedAccounts));
                model.FailedAccounts = new List<ResultAccountDTO>();
                model.Accounts = _accountInfoService.GetAllAccountInfosByYear(model.Year);
                ViewData["Message"] = "Endringene er lagret.";
                return View("FailedAccounts", model);
            }
            catch (Exception)
            {
                model.Accounts = _accountInfoService.GetAllAccountInfosByYear(model.Year);
                ViewData["Message"] = "Endringene er lagret.";
                return View("FailedAccounts", model);
            }
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