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

        public bool ValidateUserPassword(string userName, string password)
        {
            var user = _userRepository.GetOneUserByUsername(userName) ?? new UserDTO();
            return user.Password == password && !user.Disabled;
        }

        public void UpdatePassword(string userName, string newPassword)
        {
            var user = _userRepository.GetOneUserByUsername(userName);
            user.Password = newPassword;
            _userRepository.UpdateOneUser(user, false);
        }

        public bool HasUserAccessTo(string callingController, string callingAction, IEnumerable<string> accessGroups)
        {
            try
            {
                if (UserGotAccessToHoleController(callingController, accessGroups))
                    return true;
                if (UserGotAccessToControllerAndAction(callingController, callingAction, accessGroups))
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }  
        }

        private bool UserGotAccessToControllerAndAction(string callingController, string callingAction, IEnumerable<string> accessGroups)
        {
            var controller = callingController.ToUpper();
            var action = "_" + callingAction.ToUpper();
            var accessGroupName = "CONTROLLER_" + controller + action;

            if (accessGroups.Contains(accessGroupName))
                return true;
            return false;
        }

        private bool UserGotAccessToHoleController(string callingController, IEnumerable<string> accessGroups)
        {
            var controller = callingController.ToUpper();
            var accessGroupName = "CONTROLLER_" + controller;

            if (accessGroups.Contains(accessGroupName))
                return true;
            return false;
        }
    }
}
