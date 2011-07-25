using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IMenuRepository
    {
        IEnumerable<MenuItemDTO> GetAllItems();
    }
}
