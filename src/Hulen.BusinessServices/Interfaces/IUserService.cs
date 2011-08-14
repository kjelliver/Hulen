using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAllUsers();
        StorageResult SaveOneUser(UserDTO user);
        UserDTO GetOneUser(string username);
        StorageResult UpdateOneUser(UserDTO user, bool changedUsername);
        StorageResult DeleteOneUserByUserName(string username);
        bool ValidateUserPassword(string userName, string password);
        IEnumerable<string> GetAllRoles();
        void UpdatePassword(string userName, string newPassword);
        bool HasUserAccessTo(string username, string callingController, string callingAction);
    }
}
