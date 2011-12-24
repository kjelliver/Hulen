using System;
using System.Collections.Generic;
using Hulen.BusinessServices.Interfaces;
using Hulen.Storage.Interfaces;

namespace Hulen.BusinessServices.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<string> GetAllRoles()
        {
            var names = new List<string>();
            var roles = _roleRepository.GetAll();

            foreach(var role in roles)
            {
                names.Add(role.Name);
            }
            return names;
        }
    }
}