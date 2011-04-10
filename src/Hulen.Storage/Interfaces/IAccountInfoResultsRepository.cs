using System.Collections.Generic;
using System.Linq;
using Hulen.Objects.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IAccountInfoResultsRepository
    {
        IEnumerable<AccountInfoResultsDTO> GetAll();
    }
}
