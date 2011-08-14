using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.WebCode.Controllers;
using Hulen.WebCode.Models;
using Moq;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.WebCode
{
    [TestFixture]
    public class UserAdminControllerTests
    {
        private UserAdminController _subject;
        private Mock<IUserService> _userServiceMock;

        [SetUp]
        public void SetUp()
        {
            _userServiceMock = new Mock<IUserService>();
            _subject = new UserAdminController(_userServiceMock.Object);
        }

        [Test]
        public void IndexShouldReturnRightView()
        {
            var result = _subject.Index("");
            Assert.That(result.ViewName == "Index");
        }

        [Test]
        public void IndexShouldFetchAllUsers()
        {
            _userServiceMock.Setup(x => x.GetAllUsers()).Returns(new List<UserDTO> { new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false }, new UserDTO { Username = "user2", Password = "pass2", Name = "name2", Disabled = true }, new UserDTO { Username = "user3", Password = "pass3", Name = "name3", Disabled = false } });
            var result = _subject.Index("");
            var model = (UserWebModel)result.ViewData.Model;
            Assert.That(model.Users.Count(), Is.EqualTo(3));
            _userServiceMock.Verify(x => x.GetAllUsers(), Times.Once());
        }

        [Test]
        public void CreateShouldreturnRightView()
        {
            var result = _subject.Create();
            Assert.That(result.ViewData.Model, Is.InstanceOf(typeof (UserWebModel)));
            Assert.That(result.ViewName == "Create");
        }

        [Test]
        public void OtherCreateShouldAcceptPostVerbOnly()
        {
            Expression<Action<UserAdminController>> expression = c => c.Create(new UserWebModel());
            var methodCall = expression.Body as MethodCallExpression;
            if (methodCall != null)
            {
                var acceptVerbs =
                    (AcceptVerbsAttribute[]) methodCall.Method.GetCustomAttributes(typeof (AcceptVerbsAttribute), false);
                Assert.That(acceptVerbs, !Is.EqualTo(null));
                Assert.That(acceptVerbs.Length, Is.EqualTo(1));
                Assert.That(acceptVerbs[0].Verbs.First(), Is.EqualTo("POST"));
            }
        }

        [Test]
        public void InvalidModelShoulReturnCreateView()
        {
            _subject.ModelState.AddModelError("key", "Model is invalid");

            var result = _subject.Create(new UserWebModel());
            Assert.That(result.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void SuccessfulStorageShouldReturnCreateWithMessage()
        {
            var model = new UserWebModel
                            {
                                User =
                                    new UserDTO {Username = "user1", Password = "pass1", Name = "name1", Disabled = false}
                            };

            _userServiceMock.Setup(x => x.SaveOneUser(model.User)).Returns(StorageResult.Success);
            var result = _subject.Create(model);
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Brukeren er opprettet"));
        }

        [Test]
        public void AllreadyExistsStorageShouldReturnToCreateWithErrorMessage()
        {
            var model = new UserWebModel
                            {
                User =
                    new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false }
            };

            _userServiceMock.Setup(x => x.SaveOneUser(model.User)).Returns(StorageResult.AllreadyExsists);
            var result = _subject.Create(model);
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Brukeren finnes fra før."));
        }

        [Test]
        public void FailedStorageReturnsRightErrorMessage()
        {
            var model = new UserWebModel
                            {
                User =
                    new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false }
            };

            _userServiceMock.Setup(x => x.SaveOneUser(model.User)).Throws(new Exception());
            var result = _subject.Create(model);
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Feil i underliggende tjenester under lagring."));
        }

        [Test]
        public void EditShouldReturnRightView()
        {
            const string username = "testuser1";
            _userServiceMock.Setup(x => x.GetOneUser(username)).Returns(new UserDTO {Username = username});
            var result = _subject.Edit(username);

            Assert.That(result.ViewData.Model, Is.InstanceOf(typeof(UserWebModel)));
            Assert.That(result.ViewName == "Edit");
            _userServiceMock.Verify(x => x.GetOneUser(username), Times.Once());
        }

        [Test]
        public void FailedGetReturnsErrorMessage()
        {
            const string username = "testuser1";
            _userServiceMock.Setup(x => x.GetOneUser(username)).Throws(new Exception());
            var result = _subject.Edit(username);
            Assert.That(result.ViewName, Is.EqualTo("Edit"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Feil i underliggende tjenester under henting av bruker."));
        }

        [Test]
        public void OtherEditShouldAcceptPostVerbOnly()
        {
            Expression<Action<UserAdminController>> expression = c => c.Edit(new UserWebModel(), "", "");
            var methodCall = expression.Body as MethodCallExpression;
            if (methodCall != null)
            {
                var acceptVerbs =
                    (AcceptVerbsAttribute[])methodCall.Method.GetCustomAttributes(typeof(AcceptVerbsAttribute), false);
                Assert.That(acceptVerbs, !Is.EqualTo(null));
                Assert.That(acceptVerbs.Length, Is.EqualTo(1));
                Assert.That(acceptVerbs[0].Verbs.First(), Is.EqualTo("POST"));
            }
        }

        [Test]
        public void InvalidModelShoulReturnEditView()
        {
            _subject.ModelState.AddModelError("key", "Model is invalid");

            var result = _subject.Edit(new UserWebModel(), "", "");
            Assert.That(result.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        public void PostEditShouldReturnSuccessWhenUpdateIsSuccessfulWhenNotChangedUsername()
        {
            var user = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
            _userServiceMock.Setup(x => x.UpdateOneUser(user, false)).Returns(StorageResult.Success);

            var result = _subject.Edit(new UserWebModel { User = user, UserNameStoredInDb = "user1" }, "", "");

            Assert.That(result.ViewName, Is.EqualTo("Edit"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Brukeren er endret."));
        }

        [Test]
        public void PostEditShouldReturnAllreadyExcistsWhenUpdateIsUpdatingUsername()
        {
            var user = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
            _userServiceMock.Setup(x => x.UpdateOneUser(user, true)).Returns(StorageResult.AllreadyExsists);

            var result = _subject.Edit(new UserWebModel {User = user, UserNameStoredInDb = "old"}, "", "");

            Assert.That(result.ViewName, Is.EqualTo("Edit"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Brukernavn finnes fra før."));
        }

        [Test]
        public void FailedUpdateReturnsErrorMessage()
        {
            var user = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
            _userServiceMock.Setup(x => x.UpdateOneUser(user, true)).Throws(new Exception());
            var result = _subject.Edit(new UserWebModel { User = user, UserNameStoredInDb = "old" }, "", "");
            Assert.That(result.ViewName, Is.EqualTo("Edit"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Feil i underliggende tjenester under lagring av bruker."));
        }

        [Test]
        public void DeleteShouldRedirectToRightView()
        {
            var user = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
            var result = _subject.Delete(user.Username);
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void DeleteShouldReturnRightViewAndMessageWhenSuccessfulDeleting()
        {
            var user = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
            _userServiceMock.Setup(x => x.DeleteOneUserByUserName(user.Username)).Returns(StorageResult.Success);
            var result = _subject.Delete(user.Username);
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Brukerenkontoen til user1 er slettet."));
        }

        [Test]
        public void DeleteShouldReturnRightViewAndMessageWhenFailedDeleting()
        {
            var user = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
            _userServiceMock.Setup(x => x.DeleteOneUserByUserName(user.Username)).Returns(StorageResult.Failed);
            var result = _subject.Delete(user.Username);
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Feil i underliggende tjenester ved sletting av brukerkontoen til user1."));
        }

        [Test]
        public void DeleteShouldReturnRightViewAndMessageWhenCatchingException()
        {
            var user = new UserDTO { Username = "user1", Password = "pass1", Name = "name1", Disabled = false };
            _userServiceMock.Setup(x => x.DeleteOneUserByUserName(user.Username)).Throws(new Exception());
            var result = _subject.Delete(user.Username);
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Feil i underliggende tjenester ved sletting av brukerkontoen til user1."));
        }
    }
}
