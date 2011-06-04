using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;

namespace Hulen.Storage.Interfaces
{
    public interface IUserRepository
    {
        StorageResult SaveOneUser(UserDTO user);
        UserDTO GetOneUserByUsername(string username);
        void DeleteOneUser(UserDTO user);
        IEnumerable<UserDTO> GetAllUsers();
    }
}
