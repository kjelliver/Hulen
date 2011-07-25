using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Hulen.BusinessServices.Interfaces;
using Hulen.WebCode.Controllers;
using Hulen.WebCode.Models;
using Moq;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.WebCode
{
    [TestFixture]
    public class LogInControllerTests
    {
        private LogInController _subject;
        private Mock<IUserService> _userService;
        private Mock<HttpRequestBase> _httpRequest;
        private Mock<HttpContextBase> _httpContext;

        [SetUp]
        public void SetUp()
        {
            _userService = new Mock<IUserService>();
            _subject = new LogInController(_userService.Object);
            _httpRequest = new Mock<HttpRequestBase>();
            _httpContext = new Mock<HttpContextBase>();
            _httpContext.Setup(c => c.Request).Returns(_httpRequest.Object);
            _subject.ControllerContext = new ControllerContext(_httpContext.Object, new RouteData(), _subject);
        }

        [Test]
        public void LogOnShouldReturnRightView()
        {
            var result = _subject.LogIn();
            Assert.That(result.ViewName, Is.EqualTo("LogIn"));
        }

        [Test]
        public void HttpPostLogInShouldReturnReightViewWhenSuccessfullLogIn()
        {
            var model = new LogInModel {UserName = "test@test.no", Password = "12345"};
            _userService.Setup(x => x.ValidateUserPassword(model.UserName, model.Password)).Returns(true);
            var result = (RedirectToRouteResult) _subject.LogIn(model);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
        }

        [Test]
        public void HttpPostShouldReturnRightViewWithMessageWhenFailedLogin()
        {
            var model = new LogInModel { UserName = "test@test.no", Password = "12345" };
            _userService.Setup(x => x.ValidateUserPassword(model.UserName, model.Password)).Returns(false);
            var result = (RedirectToRouteResult)_subject.LogIn(model);

            Assert.That(_subject.TempData["Message"], Is.EqualTo("Feil i brukernavn eller passord"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("LogIn"));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("LogIn"));    
        }
    }
}
