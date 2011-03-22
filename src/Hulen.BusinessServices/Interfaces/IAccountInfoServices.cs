using System;
using System.Collections.Generic;
using Hulen.BusinessServices.ViewModels;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IAccountInfoServices
    {
        ICollection<AccountInfoViewModel> GetAllAccounts();
        void StoreNewAccountInfo(AccountInfoViewModel account);
        AccountInfoViewModel GetAccountById(Guid id);
        void UpdateAccountInfo(AccountInfoViewModel accountInfoViewModel);
        void Delete(AccountInfoViewModel accountInfo);
    }
}