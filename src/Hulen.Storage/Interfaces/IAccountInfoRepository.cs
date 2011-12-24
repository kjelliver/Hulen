using System;
using System.Collections.Generic;
using Hulen.Storage.DTO;
using Hulen.Utils.Enum;

namespace Hulen.Storage.Interfaces
{
    public interface IAccountInfoRepository
    {
        StorageResult SaveOne(AccountInfoDTO accountCategory);
        void SaveMeny(IEnumerable<AccountInfoDTO> accounts);
        AccountInfoDTO GetOneById(Guid id);
        IEnumerable<AccountInfoDTO> GetAllByYear(int year);
        StorageResult DeleteOne(AccountInfoDTO accountCategory);
        StorageResult DeleteOneById(Guid id);
        void DeleteAllByYear(int year);
        AccountInfoDTO GetOneByAccountNumber(int accountNumber);
        void UpdateOne(AccountInfoDTO accountCategory);
        IEnumerable<int> GetAllAccountNumbersByYear(int year);
    }
}
