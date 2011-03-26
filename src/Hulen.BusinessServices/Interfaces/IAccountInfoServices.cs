using System;
using System.Collections.Generic;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IAccountInfoServices
    {
        ICollection<AccountInfoDTO> GetAllAccounts();
        void StoreNewAccountInfo(AccountInfoDTO account);
        AccountInfoDTO GetAccountById(Guid id);
        void UpdateAccountInfo(AccountInfoDTO accountInfoViewModel);
        void Delete(AccountInfoDTO accountInfo);
    }
}