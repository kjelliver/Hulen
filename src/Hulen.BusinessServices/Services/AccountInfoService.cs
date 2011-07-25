using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using Excel;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Objects.ViewModels;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Hulen.BusinessServices.Services
{
    public class AccountInfoService : IAccountInfoService
    {
        private readonly IAccountInfoRepository _accountInfoRepository; 

        public AccountInfoService(IAccountInfoRepository accountInfoRepository)
        {
            _accountInfoRepository = accountInfoRepository;
        }

        public IEnumerable<AccountInfoViewModel> GetAllAccountInfosByYear(int year)
        {
            return MapMenyForView(_accountInfoRepository.GetAllByYear(year));
        }

        //public AccountInfoViewModel GetOneAccountInfoById(Guid id)
        //{
        //    return _accountInfoModelMapper.MapOneForView(_accountInfoRepository.GetOneById(id));
        //}

        //public IEnumerable<AccountInfoViewModel> GetAllAccountInfos()
        //{
        //    return _accountInfoModelMapper.MapMenyForView(_accountInfoRepository.GetAll());
        //}

        //public void SaveOneAccountInfo(AccountInfoViewModel accountInfoModel)
        //{
        //    _accountInfoRepository.SaveOne(_accountInfoModelMapper.MapOneForDataBase(accountInfoModel));
        //}

        //public void UpdateOneAccountInfo(AccountInfoViewModel accountInfo)
        //{
        //    var test = _accountInfoModelMapper.MapOneForDataBase(accountInfo);
        //    _accountInfoRepository.UpdateOne(test);
        //}

        //public void DeleteOneAccountInfo(AccountInfoViewModel accountInfo)
        //{
        //    _accountInfoRepository.DeleteOne(_accountInfoModelMapper.MapOneForDataBase(accountInfo));
        //}

        //public void DeleteOneAccountInfoById(Guid id)
        //{
        //    _accountInfoRepository.DeleteOne(_accountInfoRepository.GetOneById(id));
        //}

        //public void DeleteAllAccountInfosByYear(int year)
        //{
        //    _accountInfoRepository.DeleteExistingAccountInfo();
        //}

        //public void ImportFile(Stream inputStream, string year)
        //{
        //    DeleteAllAccountInfosByYear(Convert.ToInt32(year));
        //    var dataSet = ConvertStreamToDataSet(inputStream);
        //    List<AccountInfoDTO> allAccountInfos = ConvertDataSetToObjectCollection(dataSet, Convert.ToInt32(year));
        //    _accountInfoRepository.SaveMeny(allAccountInfos);
        //}

        //private static DataSet ConvertStreamToDataSet(Stream filestream)
        //{
        //    IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(filestream);
        //    return reader.AsDataSet();
        //}

        //private static List<AccountInfoDTO> ConvertDataSetToObjectCollection(DataSet dataSet, int year)
        //{
        //    var allAccountInfos = new List<AccountInfoDTO>();

        //    foreach (DataRow row in dataSet.Tables["AccountInfo"].Rows)
        //    {
        //        if (row[0].ToString() != "")
        //        {
        //            var newAccountInfo = new AccountInfoDTO();
        //            newAccountInfo.AccountNumber = Convert.ToInt32(row[0].ToString());
        //            newAccountInfo.AccountName = "Udefinert";
        //            newAccountInfo.ResultReportCategory = Convert.ToInt32(row[2].ToString());
        //            newAccountInfo.PartsReportCategory = Convert.ToInt32(row[3].ToString());
        //            newAccountInfo.WeekCategory = Convert.ToInt32(row[4].ToString());
        //            newAccountInfo.IsIncome = Convert.ToBoolean(Convert.ToInt32(row[5].ToString()));
        //            newAccountInfo.Year = year;
        //            allAccountInfos.Add(newAccountInfo);
        //        }
        //    }
        //    return allAccountInfos;
        //}

        public AccountInfoDTO MapOneForDataBase(AccountInfoViewModel account)
        {
            return new AccountInfoDTO
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                AccountName = account.AccountName,
                ResultReportCategory = (int)Enum.Parse(typeof(ResultReportCategory), account.ResultReportCategory),
                PartsReportCategory = (int)Enum.Parse(typeof(PartsReportCategory), account.PartsReportCategory),
                WeekCategory = (int)Enum.Parse(typeof(WeekCategory), account.WeekCategory),
                IsIncome = Convert.ToBoolean(account.IsIncome),
                Year = account.Year
            };
        }

         public AccountInfoViewModel MapOneForView(AccountInfoDTO accountInfo)
         {
             return new AccountInfoViewModel
                        {
                            Id = accountInfo.Id,
                            AccountNumber = accountInfo.AccountNumber,
                            AccountName = accountInfo.AccountName,
                            ResultReportCategory = ((ResultReportCategory)accountInfo.ResultReportCategory).ToString(),
                            PartsReportCategory = ((PartsReportCategory)accountInfo.PartsReportCategory).ToString(),
                            WeekCategory = ((WeekCategory)accountInfo.WeekCategory).ToString(),
                            IsIncome = Convert.ToBoolean(accountInfo.IsIncome).ToString(),
                            Year = accountInfo.Year
                        };
        }

         public ICollection<AccountInfoViewModel> MapMenyForView(IEnumerable<AccountInfoDTO> accountInfos)
         {
             var accountInfoViewModels = new Collection<AccountInfoViewModel>();

             foreach (var accountInfo in accountInfos)
             {
                 accountInfoViewModels.Add(MapOneForView(accountInfo));
             }
             return accountInfoViewModels;
         }
    }
}
