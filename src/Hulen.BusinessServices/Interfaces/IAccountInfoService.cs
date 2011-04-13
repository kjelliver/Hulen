using System;
using System.Collections.Generic;
using System.IO;
using Hulen.Objects.DTO;
using Hulen.Objects.ViewModels;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IAccountInfoService
    {
        AccountInfoViewModel GetOneAccountInfoById(Guid id);
        IEnumerable<AccountInfoViewModel> GetAllAccountInfos();
        void SaveOneAccountInfo(AccountInfoViewModel accountInfoModel);        
        void UpdateOneAccountInfo(AccountInfoViewModel accountInfo);
        void DeleteOneAccountInfo(AccountInfoViewModel accountInfo);
        void DeleteOneAccountInfoById(Guid id);
        void DeleteAllAccountInfosByYear(int year);
        void ImportFile(Stream inputStream, string year);
    }
}