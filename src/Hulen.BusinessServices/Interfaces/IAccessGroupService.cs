using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Objects.Model;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IAccessGroupService
    {
        List<AccessGroup> GetAllAccessGroups();
        StorageResult SaveOneAccessGroup(AccessGroup accessGroup);
        AccessGroup GetOneAccessGroup(Guid id);
        StorageResult UpdateOneAccessGroup(AccessGroup accessGroup);
        StorageResult DeleteOneAccessGroup(AccessGroup accessGroup);
        IEnumerable<AccessGroup> GetAccessGroupsByType(string type);
        AccessGroup GetAccessGroupByName(string name);
        IEnumerable<string> GetAccessGroupsForUser(UserDTO user);
    }
}
