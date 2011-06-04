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
        private readonly IUserRepository _repository;

        public UserService()
        {
            _repository = new UserRepository();
        }

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }

        public StorageResult SaveOneUser(UserDTO user)
        {
            return _repository.SaveOneUser(user);
        }

        public UserDTO GetOneUser(string username)
        {
            return _repository.GetOneUserByUsername(username);
        }
    }
}
