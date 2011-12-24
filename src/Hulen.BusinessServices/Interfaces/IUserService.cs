using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;
using Hulen.Utils.Enum;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        StorageResult SaveOneUser(User user);
        User GetOneUser(string username);
        StorageResult UpdateOneUser(User user, bool changedUsername);
        StorageResult DeleteOneUserByUserName(string username);
        bool ValidateUserPassword(string userName, string password);
        void UpdatePassword(string userName, string newPassword);
    }
}
