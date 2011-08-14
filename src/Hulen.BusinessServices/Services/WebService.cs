using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.Mappers.Interfaces;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Hulen.BusinessServices.Services
{
    public class WebService : IWebService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IAccessGroupRepository _accessGroupRepository;
        private IAccessGroupMapper _accessGroupMapper;

        public WebService(IMenuRepository menuRepository, IAccessGroupRepository accessGroupRepository, IAccessGroupMapper accessGroupMapper)
        {
            _menuRepository = menuRepository;
            _accessGroupRepository = accessGroupRepository;
            _accessGroupMapper = accessGroupMapper;
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
