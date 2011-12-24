using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Interfaces;
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
    public class AccountInfoServiceTests
    {
        private AccountInfoService _subject;
        private Mock<IAccountInfoRepository> _accountInfoRepositoryMock;
        private IEnumerable<AccountInfoDTO> _accountInfoDtos;

        [SetUp]
        private void SetUp()
        {
            _accountInfoRepositoryMock = new Mock<IAccountInfoRepository>();
            _subject = new AccountInfoService(_accountInfoRepositoryMock.Object);
            _accountInfoDtos = new List<AccountInfoDTO>
                                   {
                                       new AccountInfoDTO {AccountNumber = 3001, Year = 2011, PartsReportCategory = 2},
                                       new AccountInfoDTO {AccountNumber = 3002, Year = 2011, ResultReportCategory = 2},
                                       new AccountInfoDTO {AccountNumber = 3003, Year = 2011, WeekCategory = 1}
                                   };
        }

        [Test]
        public void ShouldReturnMappedViewModelsWhenCallingGetAllByYear()
        {
            _accountInfoRepositoryMock.Setup(x => x.GetAllByYear(2011)).Returns(_accountInfoDtos);
            IEnumerable<AccountInfo> result = _subject.GetAllAccountInfosByYear(2011);
            Assert.That(result.Where(x => x.AccountNumber == 3001).FirstOrDefault().PartsReportCategory, Is.EqualTo("Arrangement"));
            Assert.That(result.Where(x => x.AccountNumber == 3002).FirstOrDefault().ResultReportCategory, Is.EqualTo("AndreInntekter"));
            Assert.That(result.Where(x => x.AccountNumber == 3003).FirstOrDefault().WeekCategory, Is.EqualTo("PublicRelations"));
        }

        [Test]
        public void ShouldReturnSuccessStorageResultWhenSuccessful()
        {
            var inputModel = new AccountInfo {AccountNumber = 3000, AccountName = "Test", IsIncome = "Utgift", PartsReportCategory= "Udefinert", ResultReportCategory = "Udefinert", WeekCategory = "Udefinert", Year = 2011};
            var toDb = new AccountInfoDTO {AccountNumber = 3000, AccountName = "Test", IsIncome = false, PartsReportCategory = 0, ResultReportCategory = 0, WeekCategory = 0, Year = 2011};
            _accountInfoRepositoryMock.Setup(x => x.SaveOne(toDb)).Returns(StorageResult.Success);
            var result = _subject.SaveOneAccountInfo(inputModel);
            Assert.That(result, Is.EqualTo(StorageResult.Success));
        }
    }
}
