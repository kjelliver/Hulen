using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;

namespace Hulen.Storage.Interfaces
{
    public interface IMenuRepository
    {
        IEnumerable<MenuItemDTO> GetAllItems();
        StorageResult SaveOne(MenuItemDTO item);
        MenuItemDTO GetOneById(Guid id);
        StorageResult UpdateOne(MenuItemDTO menuItem);
        StorageResult DeleteOne(MenuItemDTO menuItem);
    }
}
