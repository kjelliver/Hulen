using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.ViewModels;
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
        private AccessGroupController _subject;
        private List<AccessGroupViewModel> _viewModels;

        [SetUp]
        public void SetUp()
        {
            _accessGroupServiceMock = new Mock<IAccessGroupService>();
            _subject = new AccessGroupController(_accessGroupServiceMock.Object);
            _viewModels = new List<AccessGroupViewModel>();
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
        public void IndexShouldFetchAllRoles()
        {
            _accessGroupServiceMock.Setup(x => x.GetAllAccessGroups()).Returns(_viewModels);
            _subject.Index();
            _accessGroupServiceMock.Verify(x => x.GetAllRoles(), Times.Once());
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
            _accessGroupServiceMock.Setup(x => x.GetAllRoles()).Throws(new Exception());
            var result = _subject.Index();
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Feil under henting av roller."));
            Assert.That(result.ViewData.Model, Is.InstanceOf(typeof(AccessGroupIndexModel)));
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
    }
}
