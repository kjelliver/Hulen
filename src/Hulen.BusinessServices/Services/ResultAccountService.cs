using System;
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
        private readonly IResultAccountRepository _resultAccountRepository = new ResultAccountRepository();

        public void ImportFile(Stream inputStream, string month, string year)
        {
            DeleteResultByMonth(Convert.ToInt32(month), Convert.ToInt32(year));
            var dataSet = ConvertStreamToDataSet(inputStream);
            IEnumerable<ResultAccountDTO> results = ConvertDataSetToObjectCollection(dataSet, Convert.ToInt32(month), Convert.ToInt32(year));
            _resultAccountRepository.SaveMeny(results);
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
                    temp.RealAccount = null;
                    results.Add(temp);
                }
            }
            return results;
        }
    }
}
