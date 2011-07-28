using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.ViewModels;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IAccessGroupService
    {
        List<AccessGroupViewModel> GetAllAccessGroups();
        List<string> GetAllRoles();
    }
}
