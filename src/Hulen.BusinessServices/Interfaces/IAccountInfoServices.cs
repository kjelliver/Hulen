using System.Collections.Generic;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IAccountInfoServices
    {
        ICollection<AccountInfoDTO> GetAllAccountInfo();
        void StoreNewAccountInfo(int accountNr, string accountName, int rrCat, int prCat, int wCat, bool isIncome);
        void OwerwriteAllAccountInfo(string filepath);
    }
}