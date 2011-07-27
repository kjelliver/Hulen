using System;
using System.Collections.Generic;
using System.IO;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Objects.ViewModels;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IAccountInfoService
    {
        AccountInfoViewModel GetOneAccountInfoById(Guid id);    
        void UpdateOneAccountInfo(AccountInfoViewModel accountInfo);
        void DeleteAllAccountInfosByYear(int year);
        void ImportFile(Stream inputStream, string year);
        IEnumerable<AccountInfoViewModel> GetAllAccountInfosByYear(int year);
        StorageResult SaveOneAccountInfo(AccountInfoViewModel accountInfo);
        StorageResult DeleteOneAccountInfoById(Guid id);
        void CopyAccountInfo(int fromYear, int toYear);
    }
}