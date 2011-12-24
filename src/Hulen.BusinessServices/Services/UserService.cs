using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;
using Hulen.Utils.Enum;

namespace Hulen.BusinessServices.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserModelMapper _userModelMapper;

        public UserService(IUserRepository userRepository, IUserModelMapper userModelMapper)
        {
            _userRepository = userRepository;
            _userModelMapper = userModelMapper;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var result = new List<User>();
            var fromDb = _userRepository.GetAllUsers();
            foreach(var dto in fromDb)
            {
                result.Add(_userModelMapper.FromDTO(dto));
            }
            return result;
        }

        public StorageResult SaveOneUser(User user)
        {
            return _userRepository.SaveOneUser(_userModelMapper.ToDTO(user));
        }

        public User GetOneUser(string username)
        {
            return  _userModelMapper.FromDTO(_userRepository.GetOneUserByUsername(username));
        }

        public StorageResult UpdateOneUser(User user, bool changedUsername)
        {
            return _userRepository.UpdateOneUser(_userModelMapper.ToDTO(user), changedUsername);
        }

        public StorageResult DeleteOneUserByUserName(string username)
        {
            return _userRepository.DeleteOneUser(username);
        }

        public bool ValidateUserPassword(string userName, string password)
        {
            var user = _userRepository.GetOneUserByUsername(userName) ?? new UserDTO();
            return user.Password == password && !user.Disabled;
        }

        public void UpdatePassword(string userName, string newPassword)
        {
            var user = _userRepository.GetOneUserByUsername(userName);
            user.Password = newPassword;
            user.MustChangePassword = false;
            _userRepository.UpdateOneUser(user, false);
        }
    }
}
