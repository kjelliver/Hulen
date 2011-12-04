using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.Models;

namespace Hulen.Objects.Mappers.Interfaces
{
    public interface IMapAccessGroup
    {
        AccessGroupDTO ToDTO(AccessGroup model);
        AccessGroup ToViewModel(AccessGroupDTO dto);
    }
}
