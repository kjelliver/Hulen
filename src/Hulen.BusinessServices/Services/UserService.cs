using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Hulen.BusinessServices.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public StorageResult SaveOneUser(UserDTO user)
        {
            return _userRepository.SaveOneUser(user);
        }

        public UserDTO GetOneUser(string username)
        {
            return _userRepository.GetOneUserByUsername(username);
        }

        public StorageResult UpdateOneUser(UserDTO user, bool changedUsername)
        {
            return _userRepository.UpdateOneUser(user, changedUsername);
        }

        public StorageResult DeleteOneUserByUserName(string username)
        {
            return _userRepository.DeleteOneUser(username);
        }
    }
}
