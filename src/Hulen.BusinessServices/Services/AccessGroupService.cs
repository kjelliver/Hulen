using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Objects.Mappers.Interfaces;
using Hulen.Objects.Model;
using Hulen.Storage.Interfaces;

namespace Hulen.BusinessServices.Services
{
    public class AccessGroupService : IAccessGroupService
    {
        private readonly IAccessGroupRepository _accessGroupRepository;
        private readonly IMapAccessGroup _accessGroupMapper;

        public AccessGroupService(IAccessGroupRepository repository, IMapAccessGroup mapper)
        {
            _accessGroupRepository = repository;
            _accessGroupMapper = mapper;
        }

        public List<AccessGroup> GetAllAccessGroups()
        {
            return _accessGroupRepository.GetAll().Select(dto => _accessGroupMapper.ToViewModel(dto)).ToList();
        }

        public StorageResult SaveOneAccessGroup(AccessGroup accessGroup)
        {
            return _accessGroupRepository.SaveOne(_accessGroupMapper.ToDTO(accessGroup));
        }

        public AccessGroup GetOneAccessGroup(Guid id)
        {
            return _accessGroupMapper.ToViewModel(_accessGroupRepository.GetOne(id));
        }

        public StorageResult UpdateOneAccessGroup(AccessGroup accessGroup)
        {
            return _accessGroupRepository.UpdateOne(_accessGroupMapper.ToDTO(accessGroup));
        }

        public StorageResult DeleteOneAccessGroup(AccessGroup accessGroup)
        {
            return _accessGroupRepository.DeleteOne(_accessGroupMapper.ToDTO(accessGroup));
        }

        public IEnumerable<AccessGroup> GetAccessGroupsByType(string type)
        {
            return _accessGroupRepository.GetAllByType(type).Select(dto => _accessGroupMapper.ToViewModel(dto)).ToList();
        }

        public AccessGroup GetAccessGroupByName(string name)
        {
            return _accessGroupMapper.ToViewModel(_accessGroupRepository.GetOneByName(name));
        }

        public IEnumerable<string> GetAccessGroupsForUser(UserDTO user)
        {
            var allAccessGroups = _accessGroupRepository.GetAll();
            var result = new List<string>();

            foreach(var accessGroup in allAccessGroups)
            {
                if (_accessGroupMapper.ToViewModel(accessGroup).RolesThatHaveAccess.Contains(user.Role))
                    result.Add(accessGroup.Name);
            }

            return result;
        }
    }
}
