
using System.Collections.Generic;
using Hulen.Objects.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IAccountInfoPartsRepository
    {
        ICollection<AccountInfoPartsDTO> GetAll();
    }
}
