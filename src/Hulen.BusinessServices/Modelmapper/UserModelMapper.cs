using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Modelmapper
{
    public class UserModelMapper : IUserModelMapper
    {
        public UserDTO ToDTO(User model)
        {
            return new UserDTO
                       {
                            Id = model.Id,
                            Username = model.Username,
                            Password = model.Password,
                            Name = model.Name,
                            Role = model.Role,
                            Disabled = model.Disabled,
                            MustChangePassword = model.MustChangePassword
                       };
        }

        public User FromDTO(UserDTO dto)
        {
            return new User
                       {
                           Id = dto.Id,
                           Username = dto.Username,
                           Password = dto.Password,
                           Name = dto.Name,
                           Role = dto.Role,
                           Disabled = dto.Disabled,
                           MustChangePassword = dto.MustChangePassword    
                       };
        }
    }
}
