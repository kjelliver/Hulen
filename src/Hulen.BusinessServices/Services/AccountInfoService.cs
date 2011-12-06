using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using Excel;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Objects.Model;
using Hulen.Storage.Interfaces;

namespace Hulen.BusinessServices.Services
{
    public class AccountInfoService : IAccountInfoService
    {
        private readonly IAccountInfoRepository _accountInfoRepository; 

        public AccountInfoService(IAccountInfoRepository accountInfoRepository)
        {
            _accountInfoRepository = accountInfoRepository;
        }

        public IEnumerable<AccountInfo> GetAllAccountInfosByYear(int year)
        {
            return MapMenyForView(_accountInfoRepository.GetAllByYear(year));
        }

        public StorageResult SaveOneAccountInfo(AccountInfo accountInfo)
        {
            return _accountInfoRepository.SaveOne(MapOneForDataBase(accountInfo));
        }

        public StorageResult DeleteOneAccountInfoById(Guid id)
        {
            return _accountInfoRepository.DeleteOneById(id);
        }

        public void CopyAccountInfo(int fromYear, int toYear)
        {
            var toAccounts = new List<AccountInfoDTO>();
            var fromAccounts = _accountInfoRepository.GetAllByYear(fromYear);
            foreach(AccountInfoDTO account in fromAccounts)
            {
                AccountInfoDTO newAccount = account;
                newAccount.Year = toYear;
                toAccounts.Add(newAccount);
            }
            _accountInfoRepository.SaveMeny(toAccounts);
        }

        public AccountInfo GetOneAccountInfoById(Guid id)
        {
            return MapOneForView(_accountInfoRepository.GetOneById(id));
        }

        public void UpdateOneAccountInfo(AccountInfo accountInfo)
        {
            _accountInfoRepository.UpdateOne(MapOneForDataBase(accountInfo));
        }

        public void DeleteAllAccountInfosByYear(int year)
        {
        //    _accountInfoRepository.DeleteExistingAccountInfo();
        }

        public void ImportFile(Stream inputStream, string year)
        {
            DeleteAllAccountInfosByYear(Convert.ToInt32(year));
            var dataSet = ConvertStreamToDataSet(inputStream);
            IEnumerable<AccountInfoDTO> allAccountInfos = ConvertDataSetToObjectCollection(dataSet, Convert.ToInt32(year));
            _accountInfoRepository.SaveMeny(allAccountInfos);
        }

        private static DataSet ConvertStreamToDataSet(Stream filestream)
        {
            IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(filestream);
            return reader.AsDataSet();
        }

        private static IEnumerable<AccountInfoDTO> ConvertDataSetToObjectCollection(DataSet dataSet, int year)
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

        public AccountInfoDTO MapOneForDataBase(AccountInfo account)
        {
            return new AccountInfoDTO
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                AccountName = account.AccountName,
                ResultReportCategory = (int)Enum.Parse(typeof(ResultReportCategory), account.ResultReportCategory),
                PartsReportCategory = (int)Enum.Parse(typeof(PartsReportCategory), account.PartsReportCategory),
                WeekCategory = (int)Enum.Parse(typeof(WeekCategory), account.WeekCategory),
                IsIncome = Convert.ToBoolean((int)Enum.Parse(typeof(IsIncome), account.IsIncome)),
                Year = account.Year
            };
        }

        public AccountInfo MapOneForView(AccountInfoDTO accountInfo)
        {
            return new AccountInfo
                        {
                            Id = accountInfo.Id,
                            AccountNumber = accountInfo.AccountNumber,
                            AccountName = accountInfo.AccountName,
                            ResultReportCategory = ((ResultReportCategory)accountInfo.ResultReportCategory).ToString(),
                            PartsReportCategory = ((PartsReportCategory)accountInfo.PartsReportCategory).ToString(),
                            WeekCategory = ((WeekCategory)accountInfo.WeekCategory).ToString(),
                            IsIncome = ((IsIncome)Convert.ToInt32(accountInfo.IsIncome)).ToString(),
                            Year = accountInfo.Year,
                            NumberAndName = accountInfo.AccountNumber + ", " + accountInfo.AccountName
                        };
        }

        public ICollection<AccountInfo> MapMenyForView(IEnumerable<AccountInfoDTO> accountInfos)
        {
            var accountInfoViewModels = new Collection<AccountInfo>();

            foreach (var accountInfo in accountInfos)
            {
                accountInfoViewModels.Add(MapOneForView(accountInfo));
            }
            return accountInfoViewModels;
        }
    }
}
