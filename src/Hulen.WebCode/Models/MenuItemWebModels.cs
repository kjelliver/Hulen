using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;

namespace Hulen.WebCode.Models
{
    public class MenuItemWebModel
    {
        public IEnumerable<MenuItemDTO> AllMenuItems { get; set; }
        public MenuItemDTO MenuItem { get; set; }

        public List<int> MenuLevels { get; set; }
        public List<string> AccessGroups { get; set; }
        public List<string> Parents { get; set; }
    }
}
