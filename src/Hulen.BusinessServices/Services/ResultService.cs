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
using Hulen.Objects.Mappers.Interfaces;
using Hulen.Objects.ViewModels;
using Hulen.Storage.Interfaces;

namespace Hulen.BusinessServices.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IAccountInfoRepository _accountInfoRepository;
        private readonly IMapResult _resultMapper;

        public ResultService(IResultRepository resultRepository, IAccountInfoRepository accountInfoRepository, IMapResult resultMapper)
        {
            _resultRepository = resultRepository;
            _resultMapper = resultMapper;
            _accountInfoRepository = accountInfoRepository;
        }

        //public ResultDTO GetOneResultByYearAndStatus(int year, string period)
        //{
        //    return _resultRepository.GetOverviewByPeriodAndYear(year, period);
        //}

        public Result NewGetOneResultByYearAndStatus(string period, int year)
        {
            return _resultMapper.FromDTO(_resultRepository.GetOverviewByPeriodAndYear(year, period));
        }

        public void DeleteResultByYearAndStatus(int year, string period)
        {
            _resultRepository.DeleteExcistingOverview(period, year);
        }

        public IEnumerable<ResultAccountDTO> TryToImportFile(Stream inputStream, string period, string year, string comment, string usedbudget)
        {
            DeleteResultByMonth(period, Convert.ToInt32(year));
            var dataSet = ConvertStreamToDataSet(inputStream);
            IEnumerable<ResultAccountDTO> results = ConvertDataSetToObjectCollection(dataSet, (int)Enum.Parse(typeof(ResultPeriod), period), Convert.ToInt32(year));
            SaveResultModel saveModel = SortResults(results, Convert.ToInt32(year));
            _resultRepository.SaveMeny(saveModel.SavedResults);
            SaveOverview(period, Convert.ToInt32(year), comment, usedbudget);
            return saveModel.FailedResults;
        }

        private void SaveOverview(string period, int year, string comment, string usedBudget)
        {
            var result = new Result {Period = period, Year = year, Comment = comment, UsedBudget = usedBudget};
            _resultRepository.SaveNewOverview(_resultMapper.ToDTO(result));
            //_resultRepository.SaveNewOverview(new ResultDTO() {Period = period, Year = year, Comment = comment, UsedBudget = usedBudget});
        }

        public ResultAccountDTO GetOneResultAccountById(Guid id)
        {
            return _resultRepository.GetOneResultAccountById(id);
        }

        public ResultAccountDTO GetOneByAccountNumberMonthAndYear(string accountNumber, string month, string year)
        {
            return _resultRepository.GetOneByAccountNumberMonthAndYear(Convert.ToInt32(accountNumber),
                                                                              Convert.ToInt32(month),
                                                                              Convert.ToInt32(year));
        }

        public void SaveMenyResultAccounts(List<ResultAccountDTO> resultAccounts)
        {
            _resultRepository.SaveMeny(resultAccounts);
        }

        public IEnumerable<ResultAccountDTO> GetAllResultAccountsByYearAndPeriod(int year, string period)
        {
            return _resultRepository.GetResultByMonth((int) Enum.Parse(typeof (ResultPeriod), period), year);
        }

        //public IEnumerable<ResultDTO> GetOverviewByYear(int year)
        //{
        //    return _resultRepository.GetOverviewByYear(year);
        //}

        public IEnumerable<Result> NewGetOverviewByYear(int year)
        {
            return _resultMapper.ManyFromDTO(_resultRepository.GetOverviewByYear(year));
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
            _resultRepository.DeleteExcistingOverview(period, year);
            _resultRepository.DeleteExcistingAccounts((int)Enum.Parse(typeof(ResultPeriod), period), year);
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
