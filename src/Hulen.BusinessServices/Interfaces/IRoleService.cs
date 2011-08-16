using System.Collections.Generic;
using Hulen.Objects.DTO;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<string> GetAllRoles();
    }
}