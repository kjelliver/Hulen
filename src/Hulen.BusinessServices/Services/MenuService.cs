using System;
using System.Collections.Generic;
using System.Linq;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.Interfaces;
using Hulen.Utils.Enum;

namespace Hulen.BusinessServices.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IAccessGroupRepository _accessGroupRepository;
        private readonly IAccessGroupModelMapper _accessGroupMapper;
        private readonly IMenuItemModelMapper _menuItemModelMapper;

        public MenuService(IMenuRepository menuRepository, IAccessGroupRepository accessGroupRepository, IAccessGroupModelMapper accessGroupMapper, IMenuItemModelMapper menuItemModelMapper)
        {
            _menuRepository = menuRepository;
            _menuItemModelMapper = menuItemModelMapper;
            _accessGroupMapper = accessGroupMapper;
            _accessGroupRepository = accessGroupRepository;
        }

        public StorageResult SaveOneMenuItem(MenuItem menuItem)
        {
            return _menuRepository.SaveOne(_menuItemModelMapper.ToDTO(menuItem));
        }

        public MenuItem GetOneById(Guid id)
        {
            return _menuItemModelMapper.FromDTO(_menuRepository.GetOneById(id));
        }

        public StorageResult UpdateOne(MenuItem menuItem)
        {
            return _menuRepository.UpdateOne(_menuItemModelMapper.ToDTO(menuItem));
        }

        public StorageResult DeleteOneMenuItem(MenuItem menuItem)
        {
            return _menuRepository.DeleteOne(_menuItemModelMapper.ToDTO(menuItem));
        }

        public IEnumerable<MenuItem> GetAllMenuItems()
        {
            var result = new List<MenuItem>();
            var fromDb = _menuRepository.GetAllItems();
            foreach(var dto in fromDb)
            {
                result.Add(_menuItemModelMapper.FromDTO(dto));
            }
            return result;
        }

        public IEnumerable<MenuItem> GetMenuItemsForUser(User user)
        {
            var result = new List<MenuItem>();

            var allMenuItems = _menuRepository.GetAllItems();
            var allMenuAccessGroups = _accessGroupRepository.GetAll();

            foreach (var menuItem in allMenuItems)
            {
                var item = menuItem;
                if (item != null)
                {
                    var accessGroup = _accessGroupMapper.ToViewModel(allMenuAccessGroups.Where(x => x.Name == item.AccessGroup).FirstOrDefault());
                    if (accessGroup.RolesThatHaveAccess.Contains(user.Role))
                        result.Add(_menuItemModelMapper.FromDTO(item));
                }
            }
            return result.OrderBy(x => x.SortOrder);
        }
    }
}