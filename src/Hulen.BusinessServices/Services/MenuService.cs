using System;
using System.Collections.Generic;
using System.Linq;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Objects.Mappers.Interfaces;
using Hulen.Storage.Interfaces;

namespace Hulen.BusinessServices.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IAccessGroupRepository _accessGroupRepository;
        private readonly IMapAccessGroup _accessGroupMapper;

        public MenuService(IMenuRepository menuRepository, IAccessGroupRepository accessGroupRepository, IMapAccessGroup accessGroupMapper)
        {
            _menuRepository = menuRepository;
            _accessGroupMapper = accessGroupMapper;
            _accessGroupRepository = accessGroupRepository;
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

        public IEnumerable<MenuItemDTO> GetAllMenuItems()
        {
            return _menuRepository.GetAllItems();
        }

        public IEnumerable<MenuItemDTO> GetMenuItemsForUser(UserDTO user)
        {
            var result = new List<MenuItemDTO>();

            var allMenuItems = _menuRepository.GetAllItems();
            var allMenuAccessGroups = _accessGroupRepository.GetAll();

            foreach (var menuItem in allMenuItems)
            {
                var item = menuItem;
                if (item != null)
                {
                    var accessGroup = _accessGroupMapper.ToViewModel(allMenuAccessGroups.Where(x => x.Name == item.AccessGroup).FirstOrDefault());
                    if (accessGroup.RolesThatHaveAccess.Contains(user.Role))
                        result.Add(item);
                }
            }
            return result.OrderBy(x => x.SortOrder);
        }
    }
}