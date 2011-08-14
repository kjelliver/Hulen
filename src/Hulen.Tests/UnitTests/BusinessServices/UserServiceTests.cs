using System.Collections.Generic;
using System.Linq;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Storage.Interfaces;
using Moq;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.BusinessServices
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IAccessGroupService> _accessGroupService;
        private IUserService _subject;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _accessGroupService = new Mock<IAccessGroupService>();
            _subject = new UserService(_userRepositoryMock.Object, _accessGroupService.Object);
        }

        [Test]
        public void GetAllUsersCallsGetAllUsers()
        {
            var users = MockUsers();
            _userRepositoryMock.Setup(x => x.GetAllUsers()).Returns(users);

            IEnumerable<UserDTO> result = _subject.GetAllUsers();

            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(IsInCollection(result.ElementAt(0), result));
            Assert.That(IsInCollection(result.ElementAt(1), result));
            Assert.That(IsInCollection(result.ElementAt(2), result));
        }

        [Test]
        public void SaveOneUserCallsRepository()
        {
            var user = MockUser();
            _userRepositoryMock.Setup(x => x.SaveOneUser(user)).Returns(StorageResult.Success);

            var createResult = _subject.SaveOneUser(user);

            Assert.That(createResult, Is.EqualTo(StorageResult.Success));
            _userRepositoryMock.Verify(x => x.SaveOneUser(user), Times.Once()); 
        }

        [Test]
        public void UpdateOneUserCallsRepository()
        {
            var user = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
            _userRepositoryMock.Setup(x => x.UpdateOneUser(user, false)).Returns(StorageResult.AllreadyExsists);

            var updateResult = _subject.UpdateOneUser(user, false);

            Assert.That(updateResult, Is.EqualTo(StorageResult.AllreadyExsists));
            _userRepositoryMock.Verify(x => x.UpdateOneUser(user, false), Times.Once()); 
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

        private static UserDTO MockUser()
        {
            return new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
        }

        private static IEnumerable<UserDTO> MockUsers()
        {
            var users = new List<UserDTO>
                            {
                                new UserDTO {Username = "user1", Password = "pass1", Name = "name1", Disabled = false},
                                new UserDTO {Username = "user2", Password = "pass2", Name = "name2", Disabled = true},
                                new UserDTO {Username = "user3", Password = "pass3", Name = "name3", Disabled = false}
                            };
            return users;
        }

        private static bool IsInCollection(UserDTO u, IEnumerable<UserDTO> fromDb)
        {
            return fromDb.Any(d => d.Id == u.Id && d.Username == u.Username && d.Password == u.Password && d.Name == u.Name && d.Disabled == u.Disabled);
        }
    }
}
