﻿using System;
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
    }
}
