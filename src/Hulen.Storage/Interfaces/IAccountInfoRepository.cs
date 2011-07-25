using System;
using System.Collections.Generic;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;

namespace Hulen.Storage.Interfaces
{
    public interface IAccountInfoRepository
    {
        StorageResult SaveOne(AccountInfoDTO accountCategory);
        IEnumerable<AccountInfoDTO> GetAllByYear(int year);
        StorageResult DeleteOne(AccountInfoDTO accountCategory);


        //void SaveMeny(ICollection<AccountInfoDTO> accounts);
        //AccountInfoDTO GetOneById(Guid id);
        //ICollection<AccountInfoDTO> GetAll();
        //IEnumerable<AccountInfoDTO> GetAllByYear(int year);
        //AccountInfoDTO GetOneByAccountNumber(int accountNumber);
        //void UpdateOne(AccountInfoDTO accountCategory);
        //void DeleteExistingAccountInfo();
        //IEnumerable<int> GetAllAccountNumbersByYear(int year);
    }
}
