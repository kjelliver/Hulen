using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Storage.DTO;
using Hulen.Utils.Enum;

namespace Hulen.Storage.Interfaces
{
    public interface IAccessGroupRepository
    {
        IEnumerable<AccessGroupDTO> GetAll();
        StorageResult SaveOne(AccessGroupDTO acc);
        AccessGroupDTO GetOne(Guid id);
        StorageResult UpdateOne(AccessGroupDTO acc);
        StorageResult DeleteOne(AccessGroupDTO acc);
        IEnumerable<AccessGroupDTO> GetAllByType(string type);
        AccessGroupDTO GetOneByName(string name);
    }
}
