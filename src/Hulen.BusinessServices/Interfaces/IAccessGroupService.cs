﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.Enum;
using Hulen.Objects.ViewModels;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IAccessGroupService
    {
        List<AccessGroupViewModel> GetAllAccessGroups();
        StorageResult SaveOneAccessGroup(AccessGroupViewModel accessGroup);
        AccessGroupViewModel GetOneAccessGroup(Guid id);
        StorageResult UpdateOneAccessGroup(AccessGroupViewModel accessGroup);
        StorageResult DeleteOneAccessGroup(AccessGroupViewModel accessGroup);
        IEnumerable<AccessGroupViewModel> GetAccessGroupsByType(string type);
        AccessGroupViewModel GetAccessGroupByName(string name);
    }
}
