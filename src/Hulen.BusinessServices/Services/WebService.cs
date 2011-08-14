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

        public IEnumerable<MenuItemDTO> GetMenuItemsForUser(UserDTO user)
        {
            var result = new List<MenuItemDTO>();
            var menuItemsOnDatabase = _menuRepository.GetAllItems();

                result.Add(menuItemsOnDatabase.Where(x => x.Name == "Hjem").First());
                result.Add(menuItemsOnDatabase.Where(x => x.Name == "Brukeradmin").First());
                result.Add(menuItemsOnDatabase.Where(x => x.Name == "Kontoinformasjon").First());
                result.Add(menuItemsOnDatabase.Where(x => x.Name == "Filimportering").First());
                result.Add(menuItemsOnDatabase.Where(x => x.Name == "Admin").First());
                result.Add(menuItemsOnDatabase.Where(x => x.Name == "Rapporter").First());

            result.Add(menuItemsOnDatabase.Where(x => x.Name == "Rediger meny").First());
            result.Add(menuItemsOnDatabase.Where(x => x.Name == "Admin. tilgangsgrupper").First());

            return result.OrderBy(x => x.SortOrder);
        }
    }
}
