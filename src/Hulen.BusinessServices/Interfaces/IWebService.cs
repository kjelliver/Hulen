using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IWebService
    {
        IEnumerable<MenuItemDTO> GetAllMenuItems();
        IEnumerable<MenuItemDTO> GetMenuItemsForUser(UserDTO user);
    }
}
