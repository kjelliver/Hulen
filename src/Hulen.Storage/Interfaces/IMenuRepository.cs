using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Utils.Enum;
using MenuItemDTO = Hulen.Storage.DTO.MenuItemDTO;

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
