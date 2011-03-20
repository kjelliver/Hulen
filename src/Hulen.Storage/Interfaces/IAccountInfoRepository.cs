using System.Collections.Generic;
using Hulen.Storage.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IAccountInfoRepository
    {
        void Add(AccountInfo accountCategory);
        void Update(AccountInfo accountCategory);
        void Delete(AccountInfo accountCategory);
        AccountInfo GetByAccountNumber(int accountNumber);
        ICollection<AccountInfo> GetAllAccountCategories();
        void DeleteExistingAccountInfo();
        void AddMeny(ICollection<AccountInfo> accounts);
    }
}
