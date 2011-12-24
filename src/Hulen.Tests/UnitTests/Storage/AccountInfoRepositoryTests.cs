using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;
using Hulen.Utils.Enum;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.Storage
{
    [TestFixture]
    public class AccountInfoRepositoryTests
    {
        private IAccountInfoRepository _accountInfoRepository;
        private AccountInfoDTO _accountInfo1;
        private AccountInfoDTO _accountInfo2;
        private AccountInfoDTO _accountInfo3;

        [SetUp]
        public void SetUp()
        {
            _accountInfoRepository = new AccountInfoRepository();
            _accountInfo1 = new AccountInfoDTO {AccountNumber = 3001, AccountName = "testkonto1", IsIncome = true, PartsReportCategory = 1, ResultReportCategory = 1, WeekCategory = 1, Year = 2011};
            _accountInfo2 = new AccountInfoDTO { AccountNumber = 3002, AccountName = "testkonto2", IsIncome = true, PartsReportCategory = 2, ResultReportCategory = 2, WeekCategory = 2, Year = 2012 };
            _accountInfo3 = new AccountInfoDTO { AccountNumber = 3003, AccountName = "testkonto3", IsIncome = true, PartsReportCategory = 3, ResultReportCategory = 3, WeekCategory = 3, Year = 2013 };            
        }

        [Ignore]
        [Test]
        public void SaveAccountInfoSuccess()
        {
            var result1 = _accountInfoRepository.SaveOne(_accountInfo1);
            var result2 = _accountInfoRepository.SaveOne(_accountInfo1);
            var result3 = _accountInfoRepository.SaveOne(_accountInfo1);

            Assert.That(result1, Is.EqualTo(StorageResult.Success));
            Assert.That(result2, Is.EqualTo(StorageResult.Success));
            Assert.That(result3, Is.EqualTo(StorageResult.Success));
        }

        public void DeleteAccountInfoSuccess()
        {
            StorageResult result = _accountInfoRepository.DeleteOne(_accountInfo1);
            Assert.That(result, Is.EqualTo(result));
        }

        [TearDown]
        public void TearDown()
        {
            _accountInfoRepository.DeleteOne(_accountInfo1);
            _accountInfoRepository.DeleteOne(_accountInfo2);
            _accountInfoRepository.DeleteOne(_accountInfo3);
        }

        private static bool IsInCollection(AccountInfoDTO u, IEnumerable<AccountInfoDTO> fromDb)
        {
            foreach (var d in fromDb)
            {
                if (d.Id == u.Id && d.AccountNumber == u.AccountNumber && d.AccountName == u.AccountName && d.IsIncome == u.IsIncome && d.Year == u.Year && d.PartsReportCategory == u.PartsReportCategory && d.ResultReportCategory == u.ResultReportCategory && d.WeekCategory == u.WeekCategory)
                    return true;
            }
            return false;
        }
    }
}
