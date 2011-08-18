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
        private readonly IAccessGroupService _accessGroupService;

        public UserService(IUserRepository userRepository, IAccessGroupService accessGroupService)
        {
            _userRepository = userRepository;
            _accessGroupService = accessGroupService;
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

        public bool HasUserAccessTo(string username, string callingController, string callingAction)
        {
            try
            {
                if (UserGotAccessToHoleController(username, callingController))
                    return true;
                if (UserGotAccessToControllerAndAction(username, callingController, callingAction))
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }  
        }

        private bool UserGotAccessToControllerAndAction(string username, string callingController, string callingAction)
        {
            var controller = callingController.ToUpper();
            var action = "_" + callingAction.ToUpper();
            var accessGroupName = "CONTROLLER_" + controller + action;
            var accessGroup = _accessGroupService.GetAccessGroupByName(accessGroupName);

            if (accessGroup.RolesThatHaveAccess.Contains(_userRepository.GetOneUserByUsername(username).Role))
                return true;
            return false;
        }

        private bool UserGotAccessToHoleController(string username, string callingController)
        {
            var controller = callingController.ToUpper();
            var accessGroupName = "CONTROLLER_" + controller;
            var accessGroup = _accessGroupService.GetAccessGroupByName(accessGroupName);

            if (accessGroup.RolesThatHaveAccess.Contains(_userRepository.GetOneUserByUsername(username).Role))
                return true;
            return false;
        }
    }
}
