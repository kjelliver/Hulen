using System.Collections.Generic;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<string> GetAllRoles();
    }
}