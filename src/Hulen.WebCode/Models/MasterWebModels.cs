using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.Objects.DTO;

namespace Hulen.WebCode.Models
{
    public class MenuWebModel
    {
        public IEnumerable<MenuItemDTO> MenuItems { get; set; }
    }

    public class BannerWebModel
    {
        public UserDTO LoggedOnUser { get; set; }
    }
}
