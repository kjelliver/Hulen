using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
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
            ViewResult result = _subject.Index("");
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void IndexShouldReturnRightViewAndMesssageWhenNoAccountInfosFound()
        {
            _accountInfoServiceMock.Setup(x => x.GetAllAccountInfosByYear(2011)).Returns(new List<AccountInfoViewModel>());
            ViewResult result = _subject.Index("");
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Ingen kontoer funnet for gitt år."));
        }

        [Test]
        public void IndexShouldRightViewAndMessageWhenThrowingExeption()
        {
            _accountInfoServiceMock.Setup(x => x.GetAllAccountInfosByYear(2011)).Throws(new Exception());
            ViewResult result = _subject.Index("");
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("En feil oppstod, vennligst prøv på nytt."));
        }

        [Test]
        public void CreateShouldreturnRightView()
        {
            ViewResult result = _subject.Create();
            Assert.That(result.ViewData.Model, Is.InstanceOf(typeof(AccountInfoIndexModel)));
            Assert.That(result.ViewName == "Create");
        }

        [Test]
        public void OtherCreateShouldAcceptPostVerbOnly()
        {
            Expression<Action<AccountInfoController>> expression = c => c.Create(new AccountInfoEditModel());
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
            var result = _subject.Create(new AccountInfoEditModel());
            Assert.That(result.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void SuccessfulStorageShouldReturnCreateWithMessage()
        {
            var model = new AccountInfoEditModel
                            {
                AccountInfo =
                    new AccountInfoViewModel { AccountNumber = 3000, AccountName = "testkonto", ResultReportCategory = "Test", IsIncome = "Inntekt"}
            };

            _accountInfoServiceMock.Setup(x => x.SaveOneAccountInfo(model.AccountInfo)).Returns(StorageResult.Success);
            var result = _subject.Create(model);
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Kontoinformasjonen er opprettet"));
        }

        [Test]
        public void AllreadyExistsStorageShouldReturnToCreateWithErrorMessage()
        {
            var model = new AccountInfoEditModel
            {
                AccountInfo =
                    new AccountInfoViewModel { AccountNumber = 3000, AccountName = "testkonto", ResultReportCategory = "Test", IsIncome = "Inntekt" }
            };

            _accountInfoServiceMock.Setup(x => x.SaveOneAccountInfo(model.AccountInfo)).Returns(StorageResult.AllreadyExsists);
            var result = _subject.Create(model);
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Kontoinformasjon med samme nummer og år finnes fra før."));
        }

        [Test]
        public void FailedStorageReturnsRightErrorMessage()
        {
            var model = new AccountInfoEditModel
            {
                AccountInfo =
                    new AccountInfoViewModel { AccountNumber = 3000, AccountName = "testkonto", ResultReportCategory = "Test", IsIncome = "Inntekt" }
            };

            _accountInfoServiceMock.Setup(x => x.SaveOneAccountInfo(model.AccountInfo)).Throws(new Exception());
            var result = _subject.Create(model);
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.ViewData["Message"], Is.EqualTo("Feil i underliggende tjenester under lagring."));
        }
    }
}
