using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.ViewModels;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Hulen.BusinessServices.Mappers
{
    class AccountInfoViewModelMapper
    {
        private readonly string[] _result = new [] {"Udefinert"};
        private readonly string[] _parts = new[] { "Udefinert", "Bar", "Arrangement", "Personalkostnader", "PR", "Støtte og tilskudd", "Økonomi", "Driftskostnader" };
        private readonly string[] _week = new[] {"Udefinert"};
        private readonly string[] _income = new[] { "Utgift", "Inntekt" };

        public ICollection<AccountInfoViewModel> MapMenyForView(IEnumerable<AccountInfoDTO> accountInfos)
        {
            var accountInfoViewModels = new Collection<AccountInfoViewModel>();

            foreach (var accountInfo in accountInfos)
            {
                accountInfoViewModels.Add(new AccountInfoViewModel
                    {
                        Id = accountInfo.Id,
                        AccountNumber = accountInfo.AccountNumber,
                        AccountName = accountInfo.AccountName,
                        ResultReportCategory = _result[accountInfo.ResultReportCategory], 
                        PartsReportCategory = _parts[accountInfo.PartsReportCategory],
                        WeekCategory = _week[accountInfo.WeekCategory],
                        IsIncome = _income[Convert.ToInt32(accountInfo.IsIncome)]
                    });
            }
            return accountInfoViewModels;
        }

        public AccountInfoDTO MapOneForDataBase(AccountInfoViewModel account)
        {
            return new AccountInfoDTO
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                AccountName = account.AccountName,
                ResultReportCategory = FindIndex(account.ResultReportCategory, _result),
                PartsReportCategory = FindIndex(account.PartsReportCategory, _parts),
                WeekCategory = FindIndex(account.WeekCategory, _week),
                IsIncome = Convert.ToBoolean(FindIndex(account.IsIncome, _income))
            };
        }

        public AccountInfoViewModel MapOneForView(AccountInfoDTO accountInfo)
        {
            return new AccountInfoViewModel
            {
                Id = accountInfo.Id,
                AccountNumber = accountInfo.AccountNumber,
                AccountName = accountInfo.AccountName,
                ResultReportCategory = _result[accountInfo.ResultReportCategory],
                PartsReportCategory = _parts[accountInfo.PartsReportCategory],
                WeekCategory = _week[accountInfo.WeekCategory],
                IsIncome = _income[Convert.ToInt32(accountInfo.IsIncome)]
            };
        }

        private static int FindIndex(string result, string[] table)
        {
            for(int i = 0; i < table.Length; i++ )
            {
                if (table[i] == result)
                    return i;
            }
            return 0;
        }  
    }
}
