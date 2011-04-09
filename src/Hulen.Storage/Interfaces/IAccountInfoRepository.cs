using System;
using System.Collections.Generic;
using Hulen.Objects.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IAccountInfoRepository
    {
        void SaveOne(AccountInfoDTO accountCategory);
        void UpdateOne(AccountInfoDTO accountCategory);
        void DeleteOne(AccountInfoDTO accountCategory);
        AccountInfoDTO GetOneByAccountNumber(int accountNumber);
        ICollection<AccountInfoDTO> GetAll();
        void DeleteExistingAccountInfo();
        void SaveMeny(ICollection<AccountInfoDTO> accounts);
        AccountInfoDTO GetOneById(Guid id);
    }
}
