using System;
using System.Collections.Generic;
using Hulen.Objects.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IAccountInfoRepository
    {
        void SaveOne(AccountInfoDTO accountCategory);
        void SaveMeny(ICollection<AccountInfoDTO> accounts);
        AccountInfoDTO GetOneById(Guid id);
        ICollection<AccountInfoDTO> GetAll();
        AccountInfoDTO GetOneByAccountNumber(int accountNumber);
        void UpdateOne(AccountInfoDTO accountCategory);
        void DeleteOne(AccountInfoDTO accountCategory);
        void DeleteExistingAccountInfo();   
    }
}
