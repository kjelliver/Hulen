using System.Collections.Generic;
using System.Linq;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.Storage
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private IUserRepository _userRepository;
        private UserDTO _testUser1;
        private UserDTO _testUser2;
        private UserDTO _testUser3;

        [SetUp]
        public void SetUp()
        {
            _userRepository = new UserRepository();
            _userRepository.SaveOneUser(_testUser1 = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false, Role = "0", MustChangePassword = false });
            _userRepository.SaveOneUser(_testUser2 = new UserDTO { Username = "user2", Password = "pass2", Name = "name2", Disabled = true, Role = "0", MustChangePassword = false });
            _userRepository.SaveOneUser(_testUser3 = new UserDTO { Username = "user3", Password = "pass3", Name = "name3", Disabled = false, Role = "0", MustChangePassword = false });
        }

        [Test]
        public void Can_Save_And_Get_One_User()
        {
            var fromDb = _userRepository.GetOneUserByUsername("user1");
            Assert.AreEqual(_testUser1.Id, fromDb.Id);
            Assert.AreEqual(_testUser1.Username, fromDb.Username);
            Assert.AreEqual(_testUser1.Password, fromDb.Password);
            Assert.AreEqual(_testUser1.Name, fromDb.Name);
            Assert.AreEqual(_testUser1.Disabled, fromDb.Disabled);
        }

        [Test]
        public void CanDeleteUser()
        {
            var user = new UserDTO {Username = "user", Password = "pass", Name = "name", Disabled = false, Role = "Testings"};
            _userRepository.SaveOneUser(user);
            _userRepository.DeleteOneUser(user.Username);
            Assert.Null(_userRepository.GetOneUserByUsername("user"));
        }

        [Test]
        public void GettingRightStatusWhenStoringUserWithExistingUserName()
        {
            var user =  new UserDTO {Username = "user2", Password = "pass", Name = "name", Disabled = false};
            var saveResult = _userRepository.SaveOneUser(user);
            Assert.That(saveResult, Is.EqualTo(StorageResult.AllreadyExsists));
        }

        [Test]
        public void CanGetAllUsers()
        {
            var fromDb = _userRepository.GetAllUsers();
            Assert.That(fromDb.Count(), Is.GreaterThanOrEqualTo(3));
            Assert.That(IsInCollection(_testUser1, fromDb));
            Assert.That(IsInCollection(_testUser2, fromDb));
            Assert.That(IsInCollection(_testUser3, fromDb));
        }

        [Test]
        public void CanUpdateOneUser()
        {
            var user = _userRepository.GetOneUserByUsername("user1");
            user.Password = "54321";
            var result = _userRepository.UpdateOneUser(user, false);
            Assert.That(result, Is.EqualTo(StorageResult.Success));
        }

        [Test]
        public void FailingUpdateOneUser()
        {
            var user = _userRepository.GetOneUserByUsername("user1");
            user.Username = "user2";
            var result = _userRepository.UpdateOneUser(user, true);
            Assert.That(result, Is.EqualTo(StorageResult.AllreadyExsists));
        }

        [TearDown]
        public void TearDown()
        {
            _userRepository.DeleteOneUser(_testUser1.Username);
            _userRepository.DeleteOneUser(_testUser2.Username);
            _userRepository.DeleteOneUser(_testUser3.Username);
        }

        private static bool IsInCollection(UserDTO u, IEnumerable<UserDTO> fromDb)
        {
            foreach (var d in fromDb)
            {
                if (d.Id == u.Id && d.Username ==u.Username && d.Password == u.Password && d.Name == u.Name && d.Disabled == u.Disabled)
                    return true;
            }
            return false;
        }
    }
}
