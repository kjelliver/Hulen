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
        private readonly IWebService _webService;

        public MenuWebModel()
        {
            _webService = new WebService();
            GenerateMenu();
        }

        private void GenerateMenu()
        {
            MenuItems = new List<MenuItemDTO>();
            MenuItems = _webService.GetAllMenuItems();
        }

        public IEnumerable<MenuItemDTO> MenuItems { get; set; }
    }
}
