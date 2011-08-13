using System;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Storage.Interfaces;

namespace Hulen.BusinessServices.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public StorageResult SaveOneMenuItem(MenuItemDTO menuItem)
        {
            return _menuRepository.SaveOne(menuItem);
        }

        public MenuItemDTO GetOneById(Guid id)
        {
            return _menuRepository.GetOneById(id);
        }

        public StorageResult UpdateOne(MenuItemDTO menuItem)
        {
            return _menuRepository.UpdateOne(menuItem);
        }

        public StorageResult DeleteOneMenuItem(MenuItemDTO menuItem)
        {
            return _menuRepository.DeleteOne(menuItem);
        }
    }
}