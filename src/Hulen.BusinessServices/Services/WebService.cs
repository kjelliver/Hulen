using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Hulen.BusinessServices.Services
{
    public class WebService : IWebService
    {
        private readonly IMenuRepository _menuRepository;

        public WebService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public IEnumerable<MenuItemDTO> GetAllMenuItems()
        {
            return _menuRepository.GetAllItems();
        }
    }
}
