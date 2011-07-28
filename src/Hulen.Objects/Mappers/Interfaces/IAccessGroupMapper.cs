using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.ViewModels;

namespace Hulen.Objects.Mappers.Interfaces
{
    public interface IAccessGroupMapper
    {
        AccessGroupDTO ToDTO(AccessGroupViewModel viewModel);
        AccessGroupViewModel ToViewModel(AccessGroupDTO dto);
    }
}
