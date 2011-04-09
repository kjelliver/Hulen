using System;
using System.Collections.Generic;
using System.IO;
using Hulen.Objects.DTO;
using Hulen.Objects.ViewModels;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IAccountInfoServices
    {
        IEnumerable<AccountInfoViewModel> GetAllAccountInfos();
        void SaveOneAccountInfo(AccountInfoViewModel accountInfoModel);
        void SaveMenyAccountInfos(List<AccountInfoDTO> allAccountInfos);        
        AccountInfoViewModel GetOneAccountInfoById(Guid id);
        void UpdateOneAccountInfo(AccountInfoViewModel accountInfo);
        void DeleteOneAccountInfo(AccountInfoViewModel accountInfo);
        void ImportFile(Stream inputStream, string year);
        void DeleteOneAccountInfoById(Guid id);
        void DeleteAllAccountInfosByYear(int year);
    }
}