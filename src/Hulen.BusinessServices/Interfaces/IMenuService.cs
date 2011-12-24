using System;
using System.Collections.Generic;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Utils.Enum;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IMenuService
    {
        IEnumerable<MenuItem> GetAllMenuItems();
        IEnumerable<MenuItem> GetMenuItemsForUser(User user);
        StorageResult SaveOneMenuItem(MenuItem menuItem);
        MenuItem GetOneById(Guid id);
        StorageResult UpdateOne(MenuItem menuItem);
        StorageResult DeleteOneMenuItem(MenuItem menuItem);
    }
}