using System;
using System.Collections.Generic;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;

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