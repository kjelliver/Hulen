using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.Objects.DTO;
using Hulen.Objects.ViewModels;

namespace Hulen.WebCode.ModelMappers
{
    public class AccountInfoModelMapper
    {
        private readonly IDropDownService _dropDownService;
        private readonly IEnumerable<AccountInfoPartsDTO> _parts;
        private readonly IEnumerable<AccountInfoResultsDTO> _result;
        private readonly IEnumerable<AccountInfoWeekDTO> _week;
        private readonly string[] _income = new[] { "Utgift", "Inntekt" };

        public AccountInfoModelMapper()
        {
            _dropDownService = new DropDownService();
            _parts = _dropDownService.GetAllPartsDTO();
            _result = _dropDownService.GetAllResultsDTO();
            _week = _dropDownService.GetAllWeeksDTO();
        }

        public AccountInfoDTO MapOneForDataBase(AccountInfoViewModel account)
        {
            return new AccountInfoDTO
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                AccountName = account.AccountName,
                ResultReportCategory = _result.Single(x => x.Name == account.ResultReportCategory).Id,
                PartsReportCategory = _parts.Single(x => x.Name == account.PartsReportCategory).Id,
                WeekCategory = _week.Single(x => x.Name == account.WeekCategory).Id,
                IsIncome = Convert.ToBoolean(FindIndex(account.IsIncome, _income)),
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
                ResultReportCategory = _result.Single(x => x.Id == accountInfo.ResultReportCategory).Name,
                PartsReportCategory = _parts.Single(x => x.Id ==accountInfo.PartsReportCategory).Name,
                WeekCategory = _week.Single(x => x.Id == accountInfo.WeekCategory).Name,
                IsIncome = _income[Convert.ToInt32(accountInfo.IsIncome)],
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
