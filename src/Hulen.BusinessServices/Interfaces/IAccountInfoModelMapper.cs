using System.Collections.Generic;
using Hulen.Objects.DTO;
using Hulen.Objects.ViewModels;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IAccountInfoModelMapper
    {
        AccountInfoDTO MapOneForDataBase(AccountInfoViewModel account);
        AccountInfoViewModel MapOneForView(AccountInfoDTO accountInfo);
        ICollection<AccountInfoViewModel> MapMenyForView(IEnumerable<AccountInfoDTO> accountInfos);
    }
}