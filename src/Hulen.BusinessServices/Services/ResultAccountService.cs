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
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Hulen.BusinessServices.Services
{
    public class ResultAccountService : IResultAccountService
    {
        private readonly IResultAccountRepository _resultAccountRepository;
        private readonly IAccountInfoRepository _accountInfoRepository;


        public ResultAccountService(IResultAccountRepository resultAccountRepository, IAccountInfoRepository accountInfoRepository)
        {
            _resultAccountRepository = resultAccountRepository;
            _accountInfoRepository = accountInfoRepository;
        }

        public void ImportFile(Stream inputStream, string month, string year)
        {
            DeleteResultByMonth(Convert.ToInt32(month), Convert.ToInt32(year));
            var dataSet = ConvertStreamToDataSet(inputStream);
            IEnumerable<ResultAccountDTO> results = ConvertDataSetToObjectCollection(dataSet, Convert.ToInt32(month), Convert.ToInt32(year));
            SaveResultModel saveModel = SortResults(results, Convert.ToInt32(year));
            _resultAccountRepository.SaveMeny(results);
        }

        public List<ResultAccountDTO> TryToImportFile(Stream inputStream, string month, string year)
        {
            DeleteResultByMonth(Convert.ToInt32(month), Convert.ToInt32(year));
            var dataSet = ConvertStreamToDataSet(inputStream);
            IEnumerable<ResultAccountDTO> results = ConvertDataSetToObjectCollection(dataSet, Convert.ToInt32(month), Convert.ToInt32(year));
            SaveResultModel saveModel = SortResults(results, Convert.ToInt32(year));
            _resultAccountRepository.SaveMeny(saveModel.SavedResults);
            _resultAccountRepository.SaveMeny(saveModel.FailedResults);
            return saveModel.FailedResults;
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

        public void UpdateMenyResultAccounts(List<ResultAccountDTO> resultAccounts)
        {
            _resultAccountRepository.UpdateMeny(resultAccounts);
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

        private void DeleteResultByMonth(int month, int year)
        {
            _resultAccountRepository.DeleteExistingResult(_resultAccountRepository.GetResultByMonth(month, year));
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
