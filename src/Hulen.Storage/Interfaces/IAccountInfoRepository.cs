using System;
using System.Collections.Generic;
using Hulen.Storage.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IAccountInfoRepository
    {
        void Add(AccountInfoDTO accountCategory);
        void Update(AccountInfoDTO accountCategory);
        void Delete(AccountInfoDTO accountCategory);
        AccountInfoDTO GetByAccountNumber(int accountNumber);
        ICollection<AccountInfoDTO> GetAllAccountCategories();
        void DeleteExistingAccountInfo();
        void AddMeny(ICollection<AccountInfoDTO> accounts);
        AccountInfoDTO GetById(Guid id);
    }
}
