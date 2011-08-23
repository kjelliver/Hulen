using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Storage.Interfaces;

namespace Hulen.BusinessServices.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultAccountRepository;
        private readonly IAccountInfoRepository _accountInfoRepository;


        public ResultService(IResultRepository resultAccountRepository, IAccountInfoRepository accountInfoRepository)
        {
            _resultAccountRepository = resultAccountRepository;
            _accountInfoRepository = accountInfoRepository;
        }

        public IEnumerable<ResultDTO> GetOverview()
        {
            return _resultAccountRepository.GetOverview();
        }

        public ResultDTO GetOneResultByYearAndStatus(int year, string period)
        {
            return _resultAccountRepository.GetOverviewByPeriodAndYear(year, period);
        }

        public void DeleteResultByYearAndStatus(int year, string period)
        {
            _resultAccountRepository.DeleteExcistingOverview(period, year);
        }

        public IEnumerable<ResultAccountDTO> TryToImportFile(Stream inputStream, string period, string year, string comment, string usedbudget)
        {
            DeleteResultByMonth(period, Convert.ToInt32(year));
            var dataSet = ConvertStreamToDataSet(inputStream);
            IEnumerable<ResultAccountDTO> results = ConvertDataSetToObjectCollection(dataSet, (int)Enum.Parse(typeof(ResultPeriod), period), Convert.ToInt32(year));
            SaveResultModel saveModel = SortResults(results, Convert.ToInt32(year));
            _resultAccountRepository.SaveMeny(saveModel.SavedResults);
            SaveOverview(period, Convert.ToInt32(year), comment, usedbudget);
            return saveModel.FailedResults;
        }

        private void SaveOverview(string period, int year, string comment, string usedBudget)
        {
            _resultAccountRepository.SaveNewOverview(new ResultDTO() {Period = period, Year = year, Comment = comment, UsedBudget = usedBudget});
        }

        public ResultAccountDTO GetOneResultAccountById(Guid id)
        {
            return _resultAccountRepository.GetOneResultAccountById(id);
        }

        public ResultAccountDTO GetOneByAccountNumberMonthAndYear(string accountNumber, string month, string year)
        {
            return _resultAccountRepository.GetOneByAccountNumberMonthAndYear(Convert.ToInt32(accountNumber),
                                                                              Convert.ToInt32(month),
                                                                              Convert.ToInt32(year));
        }

        public void SaveMenyResultAccounts(List<ResultAccountDTO> resultAccounts)
        {
            _resultAccountRepository.SaveMeny(resultAccounts);
        }

        public IEnumerable<ResultAccountDTO> GetAllResultAccountsByYearAndPeriod(int year, string period)
        {
            return _resultAccountRepository.GetResultByMonth((int) Enum.Parse(typeof (ResultPeriod), period), year);
        }

        private SaveResultModel SortResults(IEnumerable<ResultAccountDTO> results, int year)
        {
            IEnumerable<int> validAccountNumbers = _accountInfoRepository.GetAllAccountNumbersByYear(year);

            SaveResultModel saveModel = new SaveResultModel();

            foreach(var result in results)
            {
                if (validAccountNumbers.Contains(result.AccountNumber))
                    saveModel.SavedResults.Add(result);
                else
                    saveModel.FailedResults.Add(result);
            }
            return saveModel;
        }

        public void DeleteResultByMonth(string period, int year)
        {
            _resultAccountRepository.DeleteExcistingOverview(period, year);
            _resultAccountRepository.DeleteExcistingAccounts((int)Enum.Parse(typeof(ResultPeriod), period), year);
        }

        private DataSet ConvertStreamToDataSet(Stream inputStream)
        {
            IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(inputStream);
            return reader.AsDataSet();
        }

        private IEnumerable<ResultAccountDTO> ConvertDataSetToObjectCollection(DataSet dataSet, int month, int year)
        {
            var results = new List<ResultAccountDTO>();

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                var temp = new ResultAccountDTO();
                if (dataRow[0].ToString() != "")
                {
                    temp.AccountNumber = Convert.ToInt32(dataRow[0]);
                    temp.Month = month;
                    temp.Year = year;
                    temp.AmountMonth = Convert.ToDouble(dataRow[2]);
                    temp.AmountSoFar = Convert.ToDouble(dataRow[3]);
                    temp.RealAccount = 0;
                    results.Add(temp);
                }
            }
            return results;
        }
    }

    public class SaveResultModel
    {
        public SaveResultModel()
        {
            SavedResults = new List<ResultAccountDTO>();
            FailedResults = new List<ResultAccountDTO>();
        }
        
        public List<ResultAccountDTO> SavedResults { get; set; }
        public List<ResultAccountDTO> FailedResults { get; set; }
    }
}
