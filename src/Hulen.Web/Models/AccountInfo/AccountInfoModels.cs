using System.Collections.Generic;
using Hulen.Storage.DTO;

namespace Hulen.Web.Models.AccountInfo
{
    public class AccountInfoModels
    {
        public ICollection<AccountInfoDTO> AccountInfos { get; set; }
    }
}