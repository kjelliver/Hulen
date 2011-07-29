using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.ViewModels;

namespace Hulen.BusinessServices.Services
{
    public class AccessGroupService : IAccessGroupService
    {
        public List<AccessGroupViewModel> GetAllAccessGroups()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllRoles()
        {
            return new List<string> {"Administrator", "Leder"};
        }
    }
}
