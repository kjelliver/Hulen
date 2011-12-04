using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.Enum;
using Hulen.Objects.Models;
using Hulen.WebCode.Controllers;
using Hulen.WebCode.Models;
using Moq;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.WebCode
{
    [TestFixture]
    public class AccessGroupControllerTests
    {
        private Mock<IAccessGroupService> _accessGroupServiceMock;
        private Mock<IRoleService> _roleServiceMock;
        private AccessGroupController _subject;
        private List<AccessGroup> _viewModels;

        [SetUp]
        public void SetUp()
        {
            _accessGroupServiceMock = new Mock<IAccessGroupService>();
            _roleServiceMock = new Mock<IRoleService>();
            _subject = new AccessGroupController(_accessGroupServiceMock.Object, _roleServiceMock.Object);
            _viewModels = new List<AccessGroup>();
        }

        [Test]
        public void IndexShouldFetchAllAccessGroups()
        {
            _accessGroupServiceMock.Setup(x => x.GetAllAccessGroups()).Returns(_viewModels);
            _subject.Index();
            _accessGroupServiceMock.Verify(x => x.GetAllAccessGroups(), Times.AtLeastOnce());
        }

        [Test]
        public void SuccessfulFetchShouldReturnRightView()
        {
            _accessGroupServiceMock.Setup(x => x.GetAllAccessGroups()).Returns(_viewModels);
            var result = _subject.Index();
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.ViewData.Model, Is.InstanceOf(typeof(AccessGroupIndexModel)));
        }

        [Test]
        public void FailedFetchShouldReturnIndexWithMessage()
        {
            _accessGroupServiceMock.Setup(x => x.GetAllAccessGroups()).Throws(new Exception());
            var result = _subject.Index();
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Feil under henting av tilgangsgrupper."));
            Assert.That(result.ViewData.Model, Is.InstanceOf(typeof(AccessGroupIndexModel)));
        }

        [Test]
        public void CreateShouldFetchAllRoles()
        {
            _accessGroupServiceMock.Setup(x => x.GetAllAccessGroups()).Returns(_viewModels);
            _subject.Create();
            _roleServiceMock.Verify(x => x.GetAllRoles(), Times.Once());
        }

        [Test]
        public void CreateShouldReturnRightView()
        {
            var result = _subject.Create();
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.ViewData.Model, Is.InstanceOf(typeof(AccessGroupEditModel)));
        }

        [Test]
        public void FailedFetchShouldReturnCreateWithMessage()
        {
            _roleServiceMock.Setup(x => x.GetAllRoles()).Throws(new Exception());
            var result = _subject.Create();
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Feil under henting av roller."));
            Assert.That(result.ViewData.Model, Is.InstanceOf(typeof(AccessGroupEditModel)));
        }

        [Test]
        public void OtherCreateShouldAcceptHttpPostVerbOnly()
        {
            Expression<Action<AccessGroupController>> expression = c => c.Create(new AccessGroupEditModel(), "", "", "");
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
        public void InvalidModelShoulReturnCreateView()
        {
            _subject.ModelState.AddModelError("key", "Model is invalid");
            var result = _subject.Create(new AccessGroupEditModel(), "", "", "save");
            Assert.That(result.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void SuccessfulStorageShouldReturnCreateWithMessage()
        {
            var model = new AccessGroupEditModel
            {
                AccessGroup = new AccessGroup { Name = "TEST", Description = "En testgruppe", RolesThatHaveAccess = new List<string> { "1", "2" } }
            };

            _accessGroupServiceMock.Setup(x => x.SaveOneAccessGroup(model.AccessGroup)).Returns(StorageResult.Success);
            var result = _subject.Create(model, "", "", "save");
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Tilgangsgruppen er lagret"));
            _accessGroupServiceMock.Verify(x => x.SaveOneAccessGroup(model.AccessGroup), Times.Once());
        }

        [Test]
        public void AllreadyExistsStorageShouldReturnToCreateWithErrorMessage()
        {
            var model = new AccessGroupEditModel
            {
                AccessGroup = new AccessGroup { Name = "TEST", Description = "En testgruppe", RolesThatHaveAccess = new List<string> {"1", "2"}}    
            };

            _accessGroupServiceMock.Setup(x => x.SaveOneAccessGroup(model.AccessGroup)).Returns(StorageResult.AllreadyExsists);
            var result = _subject.Create(model, "", "", "save");
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Tilgangsgruppe med samme navn finnes fra før."));
            _accessGroupServiceMock.Verify(x => x.SaveOneAccessGroup(model.AccessGroup), Times.Once());
        }

        [Test]
        public void FailedStorageReturnsRightErrorMessageAndCreateView()
        {
            var model = new AccessGroupEditModel
            {
                AccessGroup = new AccessGroup { Name = "TEST", Description = "En testgruppe", RolesThatHaveAccess = new List<string> { "1", "2" } }
            };

            _accessGroupServiceMock.Setup(x => x.SaveOneAccessGroup(model.AccessGroup)).Throws(new Exception());
            var result = _subject.Create(model, "", "", "save");
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Feil i underliggende tjenester under lagring."));
            _accessGroupServiceMock.Verify(x => x.SaveOneAccessGroup(model.AccessGroup), Times.Once());
        }

        [Test]
        public void EditShouldFetchAllRolesAndReturnRightViewAndModel()
        {
            var guid = new Guid();
            var viewModel = new AccessGroup
                                {

                                    Id = guid,
                                    Name = "AccessTest",
                                    Type = "TestGruppe",
                                    Description = "Dette er en testgruppe",
                                    RolesThatHaveAccess = new List<string> {"Administrator"}
                                };
            _accessGroupServiceMock.Setup(x => x.GetOneAccessGroup(guid)).Returns(viewModel);
            _roleServiceMock.Setup(x => x.GetAllRoles()).Returns(new List<string> { "Administrator", "Leder" });
            var result = _subject.Edit(guid);
            var resultModel = (AccessGroupEditModel) result.Model;
            Assert.That(result.ViewName, Is.EqualTo("Edit"));
            Assert.That(resultModel.AccessGroup, Is.EqualTo(viewModel));
            Assert.That(resultModel.RequestedRoles, Contains.Item("Administrator"));
            Assert.That(resultModel.AvailableRoles, Contains.Item("Leder"));
            _roleServiceMock.Verify(x => x.GetAllRoles(), Times.Once());
        }

        [Test]
        public void OtherEditeShouldAcceptHttpPostVerbOnly()
        {
            Expression<Action<AccessGroupController>> expression = c => c.Edit(new AccessGroupEditModel(), "", "", "");
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
            var result = _subject.Edit(new AccessGroupEditModel(), "", "", "save");
            Assert.That(result.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        public void SuccessfulStorageShouldReturnEditWithMessage()
        {
            var model = new AccessGroupEditModel
            {
                AccessGroup = new AccessGroup { Name = "TEST", Description = "En testgruppe", RolesThatHaveAccess = new List<string> { "1", "2" } }
            };

            _accessGroupServiceMock.Setup(x => x.UpdateOneAccessGroup(model.AccessGroup)).Returns(StorageResult.Success);
            var result = _subject.Edit(model, "", "", "save");
            Assert.That(result.ViewName, Is.EqualTo("Edit"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Tilgangsgruppen er lagret"));
            _accessGroupServiceMock.Verify(x => x.UpdateOneAccessGroup(model.AccessGroup), Times.Once());
        }

        [Test]
        public void AllreadyExistsStorageShouldReturnToEditWithErrorMessage()
        {
            var model = new AccessGroupEditModel
            {
                AccessGroup = new AccessGroup { Name = "TEST", Description = "En testgruppe", RolesThatHaveAccess = new List<string> { "1", "2" } }
            };

            _accessGroupServiceMock.Setup(x => x.UpdateOneAccessGroup(model.AccessGroup)).Returns(StorageResult.AllreadyExsists);
            var result = _subject.Edit(model, "", "", "save");
            Assert.That(result.ViewName, Is.EqualTo("Edit"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Tilgangsgruppe med samme navn finnes fra før."));
            _accessGroupServiceMock.Verify(x => x.UpdateOneAccessGroup(model.AccessGroup), Times.Once());
        }

        [Test]
        public void FailedStorageReturnsRightErrorMessageAndEditView()
        {
            var model = new AccessGroupEditModel
            {
                AccessGroup = new AccessGroup { Name = "TEST", Description = "En testgruppe", RolesThatHaveAccess = new List<string> { "1", "2" } }
            };

            _accessGroupServiceMock.Setup(x => x.UpdateOneAccessGroup(model.AccessGroup)).Throws(new Exception());
            var result = _subject.Edit(model, "", "", "save");
            Assert.That(result.ViewName, Is.EqualTo("Edit"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Feil i underliggende tjenester under lagring."));
            _accessGroupServiceMock.Verify(x => x.UpdateOneAccessGroup(model.AccessGroup), Times.Once());
        }

        [Test]
        public void DeleteShouldFetchAccessGroup()
        {
            var guid = new Guid();
            var viewModel = new AccessGroup
            {

                Id = guid,
                Name = "AccessTest",
                Type = "TestGruppe",
                Description = "Dette er en testgruppe",
                RolesThatHaveAccess = new List<string> { "Administrator" }
            };
            _accessGroupServiceMock.Setup(x => x.GetOneAccessGroup(guid)).Returns(viewModel);
            var result = _subject.Edit(guid);
            Assert.That(result.ViewName, Is.EqualTo("Edit"));
            _accessGroupServiceMock.Verify(x => x.GetOneAccessGroup(guid), Times.Once());
        }

        [Test]
        public void OtherDeleteShouldAcceptHttpPostVerbOnly()
        {
            Expression<Action<AccessGroupController>> expression = c => c.Delete(new AccessGroupEditModel());
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
        public void SuccessfulDeleteShouldReturnEditWithMessage()
        {
            var model = new AccessGroupEditModel
            {
                AccessGroup = new AccessGroup { Name = "TEST", Description = "En testgruppe", RolesThatHaveAccess = new List<string> { "1", "2" } }
            };

            _accessGroupServiceMock.Setup(x => x.DeleteOneAccessGroup(model.AccessGroup)).Returns(StorageResult.Success);
            var result = _subject.Delete(model);
            Assert.That(result.ViewName, Is.EqualTo("Delete"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Tilgangsgruppen er slettet"));
            _accessGroupServiceMock.Verify(x => x.DeleteOneAccessGroup(model.AccessGroup), Times.Once());
        }

        [Test]
        public void FailedDeleteReturnsRightErrorMessageAndEditView()
        {
            var model = new AccessGroupEditModel
            {
                AccessGroup = new AccessGroup { Name = "TEST", Description = "En testgruppe", RolesThatHaveAccess = new List<string> { "1", "2" } }
            };

            _accessGroupServiceMock.Setup(x => x.DeleteOneAccessGroup(model.AccessGroup)).Throws(new Exception());
            var result = _subject.Delete(model);
            Assert.That(result.ViewName, Is.EqualTo("Delete"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Feil i underliggende tjenester under sletting."));
            _accessGroupServiceMock.Verify(x => x.DeleteOneAccessGroup(model.AccessGroup), Times.Once());
        }

        [Test]
        public void CanAddRole()
        {
            var model = new AccessGroupEditModel
                            {
                                AvailableSelected = new[] {"Administrator"}
                            };
            _roleServiceMock.Setup(x => x.GetAllRoles()).Returns(new List<string> {"Administrator", "Leder"});
            var resultModel = (AccessGroupEditModel) _subject.Create(model, ">>", "", "").Model;
            Assert.That(resultModel.RequestedRoles, Contains.Item("Administrator"));
            Assert.That(resultModel.AvailableRoles, Contains.Item("Leder"));
        }

        [Test]
        public void CanRemoveRole()
        {
            var model = new AccessGroupEditModel
                            {
                RequestedSelected = new[] { "Administrator" }
            };
            _roleServiceMock.Setup(x => x.GetAllRoles()).Returns(new List<string> { "Administrator", "Leder" });
            var resultModel = (AccessGroupEditModel)_subject.Create(model, "", "<<", "").Model;
            Assert.That(resultModel.AvailableRoles, Contains.Item("Leder"));
            Assert.That(resultModel.AvailableRoles, Contains.Item("Administrator"));
        }
    }
}
