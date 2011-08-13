using System;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IMenuService
    {
        StorageResult SaveOneMenuItem(MenuItemDTO menuItem);
        MenuItemDTO GetOneById(Guid id);
        StorageResult UpdateOne(MenuItemDTO menuItem);
        StorageResult DeleteOneMenuItem(MenuItemDTO menuItem);
    }
}