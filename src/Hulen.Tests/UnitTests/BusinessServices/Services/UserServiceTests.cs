using System.Collections.Generic;
using System.Linq;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.BusinessServices.Services;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Utils.Enum;
using Moq;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.BusinessServices
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUserModelMapper> _userModelMapperMock;
        private Mock<IAccessGroupService> _accessGroupService;
        private IUserService _subject;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _accessGroupService = new Mock<IAccessGroupService>();
            _userModelMapperMock = new Mock<IUserModelMapper>();
            _subject = new UserService(_userRepositoryMock.Object, _userModelMapperMock.Object);
        }

        [Ignore]
        [Test]
        public void GetAllUsersCallsGetAllUsers()
        {
            var users = MockUsers();
            var usersDTO = MockUsersDTO();
            _userRepositoryMock.Setup(x => x.GetAllUsers()).Returns(usersDTO);

            IEnumerable<User> result = _subject.GetAllUsers();

            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(IsInCollection(result.ElementAt(0), result));
            Assert.That(IsInCollection(result.ElementAt(1), result));
            Assert.That(IsInCollection(result.ElementAt(2), result));
        }

        [Ignore]
        [Test]
        public void SaveOneUserCallsRepository()
        {
            var user = MockUser();
            var userDTO = MockUserDTO();
            _userRepositoryMock.Setup(x => x.SaveOneUser(userDTO)).Returns(StorageResult.Success);

            var createResult = _subject.SaveOneUser( user);

            Assert.That(createResult, Is.EqualTo(StorageResult.Success));
            _userRepositoryMock.Verify(x => x.SaveOneUser(userDTO), Times.Once()); 
        }

        [Ignore]
        [Test]
        public void UpdateOneUserCallsRepository()
        {
            var user = new User { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
            var userDTO = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };

            _userModelMapperMock.Setup(x => x.ToDTO(user)).Returns(userDTO);
            _userRepositoryMock.Setup(x => x.UpdateOneUser(userDTO, false)).Returns(StorageResult.AllreadyExsists);

            var updateResult = _subject.UpdateOneUser(user, false);

            Assert.That(updateResult, Is.EqualTo(StorageResult.AllreadyExsists));
            _userRepositoryMock.Verify(x => x.UpdateOneUser(userDTO, false), Times.Once()); 
        }

        [Test]
        public void DeleteOneUserCallsRepository()
        {
            var user = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
            _userRepositoryMock.Setup(x => x.DeleteOneUser(user.Username)).Returns(StorageResult.Success);

            var updateResult = _subject.DeleteOneUserByUserName(user.Username);

            Assert.That(updateResult, Is.EqualTo(StorageResult.Success));
            _userRepositoryMock.Verify(x => x.DeleteOneUser(user.Username), Times.Once());
        }

        [Test]
        public void SuccessfullValidationReturnsTrue()
        {
            var user = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
            _userRepositoryMock.Setup(x => x.GetOneUserByUsername(user.Username)).Returns(user);
            var result = _subject.ValidateUserPassword("user1", "pass1");
            Assert.IsTrue(result);
        }

        [Test]
        public void UserValidationReturnsFalseWhenDisabled()
        {
            var user = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = true };
            _userRepositoryMock.Setup(x => x.GetOneUserByUsername(user.Username)).Returns(user);
            var result = _subject.ValidateUserPassword("user1", "pass1");
            Assert.IsFalse(result);
        }

        [Test]
        public void UserValidationReturnsFalseWhenWrongUserNameAndPassword()
        {
            var user = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
            _userRepositoryMock.Setup(x => x.GetOneUserByUsername(user.Username)).Returns(user);
            var result = _subject.ValidateUserPassword("user1", "wrongpass");
            Assert.IsFalse(result);
        }

        private static User MockUser()
        {
            return new User { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
        }

        private static UserDTO MockUserDTO()
        {
            return new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
        }

        private static IEnumerable<User> MockUsers()
        {
            var users = new List<User>
                            {
                                new User {Username = "user1", Password = "pass1", Name = "name1", Disabled = false},
                                new User {Username = "user2", Password = "pass2", Name = "name2", Disabled = true},
                                new User {Username = "user3", Password = "pass3", Name = "name3", Disabled = false}
                            };
            return users;
        }

        private static IEnumerable<UserDTO> MockUsersDTO()
        {
            var users = new List<UserDTO>
                            {
                                new UserDTO {Username = "user1", Password = "pass1", Name = "name1", Disabled = false},
                                new UserDTO {Username = "user2", Password = "pass2", Name = "name2", Disabled = true},
                                new UserDTO {Username = "user3", Password = "pass3", Name = "name3", Disabled = false}
                            };
            return users;
        }

        private static bool IsInCollection(User u, IEnumerable<User> fromDb)
        {
            return fromDb.Any(d => d.Id == u.Id && d.Username == u.Username && d.Password == u.Password && d.Name == u.Name && d.Disabled == u.Disabled);
        }

        private static bool IsDTOInCollection(UserDTO u, IEnumerable<UserDTO> fromDb)
        {
            return fromDb.Any(d => d.Id == u.Id && d.Username == u.Username && d.Password == u.Password && d.Name == u.Name && d.Disabled == u.Disabled);
        }
    }
}
