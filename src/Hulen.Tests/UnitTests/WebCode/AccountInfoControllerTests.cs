using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.ViewModels;
using Hulen.WebCode.Controllers;
using Hulen.WebCode.Models;
using Moq;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.WebCode
{
    [TestFixture]
    public class AccountInfoControllerTests
    {
        private AccountInfoController _subject;
        private Mock<IAccountInfoService> _accountInfoServiceMock;
        private IEnumerable<AccountInfoViewModel> _accountInfos;

        [SetUp]
        public void SetUp()
        {
            _accountInfoServiceMock = new Mock<IAccountInfoService>();
            _subject = new AccountInfoController(_accountInfoServiceMock.Object);
            _accountInfos = new List<AccountInfoViewModel> {new AccountInfoViewModel {AccountNumber = 3001}, new AccountInfoViewModel {AccountNumber=3002}};
        }

        [Test]
        public void IndexReturnsRightViewWhenSuccessGetAllAccounts()
        {
            _accountInfoServiceMock.Setup(x => x.GetAllAccountInfosByYear(2011)).Returns(_accountInfos);
            ViewResult result = _subject.Index();
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void IndexShouldReturnRightViewAndMesssageWhenNoAccountInfosFound()
        {
            _accountInfoServiceMock.Setup(x => x.GetAllAccountInfosByYear(2011)).Returns(new List<AccountInfoViewModel>());
            ViewResult result = _subject.Index();
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Ingen kontoer funnet for gitt år."));
        }

        [Test]
        public void IndexShouldRightViewAndMessageWhenThrowingExeption()
        {
            _accountInfoServiceMock.Setup(x => x.GetAllAccountInfosByYear(2011)).Throws(new Exception());
            ViewResult result = _subject.Index();
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("En feil oppstod, vennligst prøv på nytt."));
        }

        [Test]
        public void CreateShouldreturnRightView()
        {
            ViewResult result = _subject.Create();
            Assert.That(result.ViewData.Model, Is.InstanceOf(typeof(AccountInfoWebModel)));
            Assert.That(result.ViewName == "Create");
        }
    }
}
