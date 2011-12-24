using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Utils.Enum;

namespace Hulen.BusinessServices.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IAccountInfoRepository _accountInfoRepository;
        private readonly IResultModelMapper _resultMapper;
        private readonly IResultAccountModelMapper _resultAccountModelMapper;

        public ResultService(IResultRepository resultRepository, IAccountInfoRepository accountInfoRepository, IResultModelMapper resultMapper, IResultAccountModelMapper resultAccountModelMapper)
        {
            _resultRepository = resultRepository;
            _resultAccountModelMapper = resultAccountModelMapper;
            _resultMapper = resultMapper;
            _accountInfoRepository = accountInfoRepository;
        }

        public Result GetOneResultByYearAndStatus(string period, int year)
        {
            return _resultMapper.FromDTO(_resultRepository.GetOverviewByPeriodAndYear((int) Enum.Parse(typeof(ResultPeriod), period), year));
        }

        public IEnumerable<ResultAccount> TryToImportFile(Stream inputStream, string period, string year, string comment, string usedbudget)
        {
            DeleteResultByMonth(period, Convert.ToInt32(year));
            var dataSet = ConvertStreamToDataSet(inputStream);
            IEnumerable<ResultAccount> results = ConvertDataSetToObjectCollection(dataSet, (int)Enum.Parse(typeof(ResultPeriod), period), Convert.ToInt32(year));
            SaveResultModel saveModel = SortResults(results, Convert.ToInt32(year));
            _resultRepository.SaveMeny(MapManyToDTO(saveModel.SavedResults));
            SaveOverview(period, Convert.ToInt32(year), comment, usedbudget);
            return saveModel.FailedResults;
        }

        private IEnumerable<ResultAccountDTO> MapManyToDTO(List<ResultAccount> savedResults)
        {
            var result = new List<ResultAccountDTO>();
            foreach(var model in savedResults)
            {
                result.Add(_resultAccountModelMapper.ToDTO(model));
            }
            return result;
        }

        private void SaveOverview(string period, int year, string comment, string usedBudget)
        {
            var result = new Result {Period = period, Year = year, Comment = comment, UsedBudget = usedBudget};
            _resultRepository.SaveNewOverview(_resultMapper.ToDTO(result));
        }

        public ResultAccount GetOneResultAccountById(Guid id)
        {
            return _resultAccountModelMapper.FromDTO(_resultRepository.GetOneResultAccountById(id));
        }

        public ResultAccount GetOneByAccountNumberMonthAndYear(string accountNumber, string month, string year)
        {
            return _resultAccountModelMapper.FromDTO(_resultRepository.GetOneByAccountNumberMonthAndYear(Convert.ToInt32(accountNumber),
                                                                              Convert.ToInt32(month),
                                                                              Convert.ToInt32(year)));
        }

        public void SaveMenyResultAccounts(List<ResultAccount> resultAccounts)
        {
            _resultRepository.SaveMeny(MapManyToDTO(resultAccounts));
        }

        public IEnumerable<ResultAccount> GetAllResultAccountsByYearAndPeriod(int year, string period)
        {
            var result = new List<ResultAccount>();
            var fromDb =  _resultRepository.GetResultByMonth((int) Enum.Parse(typeof (ResultPeriod), period), year);
            foreach(var dto in fromDb)
            {
                result.Add(_resultAccountModelMapper.FromDTO(dto));
            }
            return result;
        }

        public IEnumerable<Result> GetOverviewByYear(int year)
        {
            return _resultMapper.ManyFromDTO(_resultRepository.GetOverviewByYear(year));
        }

        private SaveResultModel SortResults(IEnumerable<ResultAccount> results, int year)
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
            _resultRepository.DeleteExcistingOverview((int) Enum.Parse(typeof(ResultPeriod), period), year);
            _resultRepository.DeleteExcistingAccounts((int)Enum.Parse(typeof(ResultPeriod), period), year);
        }

        private DataSet ConvertStreamToDataSet(Stream inputStream)
        {
            IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(inputStream);
            return reader.AsDataSet();
        }

        private IEnumerable<ResultAccount> ConvertDataSetToObjectCollection(DataSet dataSet, int month, int year)
        {
            var results = new List<ResultAccount>();

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                var temp = new ResultAccount();
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
            SavedResults = new List<ResultAccount>();
            FailedResults = new List<ResultAccount>();
        }
        
        public List<ResultAccount> SavedResults { get; set; }
        public List<ResultAccount> FailedResults { get; set; }
    }
}
