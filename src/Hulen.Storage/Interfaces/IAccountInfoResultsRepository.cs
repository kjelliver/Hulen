using System.Collections.Generic;
using Hulen.Objects.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IAccountInfoResultsRepository
    {
        ICollection<AccountInfoResultsDTO> GetAll();
    }
}
