using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Mappers;
using Hulen.BusinessServices.ViewModels;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;
using Microsoft.Office.Interop.Excel;

namespace Hulen.BusinessServices.Services
{
    public class AccountInfoServices : IAccountInfoServices
    {
        private readonly AccountInfoViewModelMapper _mapper = new AccountInfoViewModelMapper();
        private readonly IAccountInfoRepository _repository = new AccountInfoRepository();
        private readonly Application _application = new Application();


        public ICollection<AccountInfoViewModel> GetAllAccounts()
        {
            return _mapper.MapMenyForView(_repository.GetAllAccountCategories()); 
        }

        public void StoreNewAccountInfo(AccountInfoViewModel account)
        {
            _repository.Add(_mapper.MapOneForDataBase(account));
        }

        public AccountInfoViewModel GetAccountById(Guid id)
        {
            return _mapper.MapOneForView(_repository.GetById(id));
        }

        public void UpdateAccountInfo(AccountInfoViewModel accountInfoViewModel)
        {
            _repository.Update(_mapper.MapOneForDataBase(accountInfoViewModel));
        }

        public void Delete(AccountInfoViewModel accountInfo)
        {
            _repository.Delete(_mapper.MapOneForDataBase(accountInfo));
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
