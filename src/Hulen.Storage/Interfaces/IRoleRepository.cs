using System;
using System.Collections.Generic;
using Hulen.Storage.DTO;
using Hulen.Utils.Enum;

namespace Hulen.Storage.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<RoleDTO> GetAll();
        StorageResult SaveOne(RoleDTO role);
        StorageResult UpdateOne(RoleDTO role);
        RoleDTO GetOne(Guid id);
    }
}