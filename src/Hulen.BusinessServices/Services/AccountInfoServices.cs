using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Hulen.BusinessServices.Interfaces;
using Hulen.Reporting.Services;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;
using Microsoft.Office.Interop.Excel;

namespace Hulen.BusinessServices.Services
{
    public class AccountInfoServices : IAccountInfoServices
    {
        private readonly IAccountInfoRepository _repository = new AccountInfoRepository();
        private AccountInfoReport _accountInfoReporting = new AccountInfoReport();
        private readonly Application _application = new Application();


        public ICollection<AccountInfoDTO> GetAllAccounts()
        {
            return _repository.GetAllAccountCategories(); 
        }

        public void StoreNewAccountInfo(AccountInfoDTO account)
        {
            _repository.Add(account);
        }

        public AccountInfoDTO GetAccountById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void UpdateAccountInfo(AccountInfoDTO accountInfo)
        {
            _repository.Update(accountInfo);
        }

        public void Delete(AccountInfoDTO  accountInfo)
        {
            _repository.Delete(accountInfo);
        }

        public void GeneratePdf()
        {
            //GeneratePDF

            _accountInfoReporting.GeneratePdf();

            //MemoryStream ms = new MemoryStream();



            //byte[] byteInfo = pdf.Output();
            //ms.Write(byteInfo, 0, byteInfo.Length);
            //ms.Position = 0;

            //return ms;
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
