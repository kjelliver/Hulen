using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.ModelMappers;
using Hulen.Objects.DTO;
using Hulen.Objects.ViewModels;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Hulen.BusinessServices.Services
{
    public class AccountInfoService : IAccountInfoService
    {
        private readonly IAccountInfoRepository _accountInfoRepository; 
        private readonly IAccountInfoModelMapper _accountInfoModelMapper;


        public AccountInfoService(IAccountInfoRepository accountInfoRepository, IAccountInfoModelMapper accountInfoModelMapper)
        {
            _accountInfoRepository = accountInfoRepository;
            _accountInfoModelMapper = accountInfoModelMapper;
        }

        public AccountInfoViewModel GetOneAccountInfoById(Guid id)
        {
            return _accountInfoModelMapper.MapOneForView(_accountInfoRepository.GetOneById(id));
        }

        public IEnumerable<AccountInfoViewModel> GetAllAccountInfos()
        {
            return _accountInfoModelMapper.MapMenyForView(_accountInfoRepository.GetAll());
        }

        public void SaveOneAccountInfo(AccountInfoViewModel accountInfoModel)
        {
            _accountInfoRepository.SaveOne(_accountInfoModelMapper.MapOneForDataBase(accountInfoModel));
        }

        public void UpdateOneAccountInfo(AccountInfoViewModel accountInfo)
        {
            var test = _accountInfoModelMapper.MapOneForDataBase(accountInfo);
            _accountInfoRepository.UpdateOne(test);
        }

        public void DeleteOneAccountInfo(AccountInfoViewModel accountInfo)
        {
            _accountInfoRepository.DeleteOne(_accountInfoModelMapper.MapOneForDataBase(accountInfo));
        }

        public void DeleteOneAccountInfoById(Guid id)
        {
            _accountInfoRepository.DeleteOne(_accountInfoRepository.GetOneById(id));
        }

        public void DeleteAllAccountInfosByYear(int year)
        {
            _accountInfoRepository.DeleteExistingAccountInfo();
        }

        public void ImportFile(Stream inputStream, string year)
        {
            DeleteAllAccountInfosByYear(Convert.ToInt32(year));
            var dataSet = ConvertStreamToDataSet(inputStream);
            List<AccountInfoDTO> allAccountInfos = ConvertDataSetToObjectCollection(dataSet, Convert.ToInt32(year));
            _accountInfoRepository.SaveMeny(allAccountInfos);
        }

        private static DataSet ConvertStreamToDataSet(Stream filestream)
        {
            IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(filestream);
            return reader.AsDataSet();
        }

        private static List<AccountInfoDTO> ConvertDataSetToObjectCollection(DataSet dataSet, int year)
        {
            var allAccountInfos = new List<AccountInfoDTO>();

            foreach (DataRow row in dataSet.Tables["AccountInfo"].Rows)
            {
                if (row[0].ToString() != "")
                {
                    var newAccountInfo = new AccountInfoDTO();
                    newAccountInfo.AccountNumber = Convert.ToInt32(row[0].ToString());
                    newAccountInfo.AccountName = "Udefinert";
                    newAccountInfo.ResultReportCategory = Convert.ToInt32(row[2].ToString());
                    newAccountInfo.PartsReportCategory = Convert.ToInt32(row[3].ToString());
                    newAccountInfo.WeekCategory = Convert.ToInt32(row[4].ToString());
                    newAccountInfo.IsIncome = Convert.ToBoolean(Convert.ToInt32(row[5].ToString()));
                    newAccountInfo.Year = year;
                    allAccountInfos.Add(newAccountInfo);
                }
            }
            return allAccountInfos;
        }
    }
}
