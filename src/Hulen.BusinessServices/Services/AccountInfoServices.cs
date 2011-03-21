using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using Excel;
using Hulen.BusinessServices.Interfaces;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;
using Microsoft.Office.Interop.Excel;

namespace Hulen.BusinessServices.Services
{
    public class AccountInfoServices : IAccountInfoServices
    {
        private readonly IAccountInfoRepository _repository = new AccountInfoRepository();
        Application _application = new Application();


        public ICollection<AccountInfoDTO> GetAllAccountInfo()
        {
            return _repository.GetAllAccountCategories();
        }

        public void StoreNewAccountInfo(int accountNr, string accountName, int rrCat, int prCat, int wCat, bool isIncome)
        {
            var item = new AccountInfoDTO
            {
                AccountNumber = accountNr,
                AccountName = accountName,
                ResultReportCategory = rrCat,
                PartsReportCategory = prCat,
                WeekCategory = wCat,
                IsIncome = isIncome
            };
            _repository.Add(item);
        }

        public void OwerwriteAllAccountInfo(string filepath)
        {
            _repository.DeleteExistingAccountInfo();
            ICollection<AccountInfoDTO> accounts = ConvertDataSetToAccountInfoObjectCollection(filepath);
            _repository.AddMeny(accounts);
        }

        private ICollection<AccountInfoDTO> ConvertDataSetToAccountInfoObjectCollection(string filepath)
        {
            ICollection<AccountInfoDTO> accountInfos = new Collection<AccountInfoDTO>();

            Workbook workbook = _application.Workbooks.Open(filepath);
            Sheets sheets = workbook.Worksheets;
            Worksheet worksheet = (Worksheet)sheets.Item["AccountInfo"];

            Range range = worksheet.Range["A1", "F95"];
            var accountArrey = (Array)range.Cells.Value;

            for (int i = 1; i <= accountArrey.GetLength(0); i++)
            {
                if (accountArrey.GetValue(i, 1).ToString() != "")
                {
                    var accountInfo = new AccountInfoDTO();
                    accountInfo.AccountNumber = Convert.ToInt32(accountArrey.GetValue(i, 1).ToString());
                    accountInfo.AccountName = accountArrey.GetValue(i, 2).ToString();
                    accountInfo.ResultReportCategory = Convert.ToInt32(accountArrey.GetValue(i, 3).ToString());
                    accountInfo.PartsReportCategory = Convert.ToInt32(accountArrey.GetValue(i, 4).ToString());
                    accountInfo.WeekCategory = Convert.ToInt32(accountArrey.GetValue(i, 5).ToString());
                    accountInfo.IsIncome = Convert.ToBoolean(Convert.ToInt32(accountArrey.GetValue(i, 6).ToString()));
                    accountInfos.Add(accountInfo);
                }
            }
            return accountInfos;
        }
    }
}
