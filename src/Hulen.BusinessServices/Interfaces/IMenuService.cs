using System;
using System.Collections.Generic;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IMenuService
    {
        IEnumerable<MenuItemDTO> GetAllMenuItems();
        IEnumerable<MenuItemDTO> GetMenuItemsForUser(UserDTO user);
        StorageResult SaveOneMenuItem(MenuItemDTO menuItem);
        MenuItemDTO GetOneById(Guid id);
        StorageResult UpdateOne(MenuItemDTO menuItem);
        StorageResult DeleteOneMenuItem(MenuItemDTO menuItem);
    }
}