using System;
using System.Collections.Generic;
using System.IO;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Utils.Enum;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IAccountInfoService
    {
        AccountInfo GetOneAccountInfoById(Guid id);    
        void UpdateOneAccountInfo(AccountInfo accountInfo);
        void DeleteAllAccountInfosByYear(int year);
        void ImportFile(Stream inputStream, string year);
        IEnumerable<AccountInfo> GetAllAccountInfosByYear(int year);
        StorageResult SaveOneAccountInfo(AccountInfo accountInfo);
        StorageResult DeleteOneAccountInfoById(Guid id);
        void CopyAccountInfo(int fromYear, int toYear);
    }
}