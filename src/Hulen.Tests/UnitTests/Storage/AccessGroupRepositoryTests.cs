using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Storage.Repositories;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.Storage
{
    [TestFixture]
    public class AccessGroupRepositoryTests
    {
        private AccessGroupRepository _subject;
        private AccessGroupDTO _ag1;
        private AccessGroupDTO _ag2;
        private AccessGroupDTO _ag3;

        [SetUp]
        private void SetUp()
        {
            _subject = new AccessGroupRepository();
            _ag1 = new AccessGroupDTO {Name = "Test1", Type = "Testgruppe", Description = "Første testgruppe"};
            _ag2 = new AccessGroupDTO {Name = "Test1", Type = "Testgruppe", Description = "Andre testgruppe"};
            _ag3 = new AccessGroupDTO {Name = "Test3", Type = "Testgruppe", Description = "Tredje testgruppe"};

        }

        [Test]
        public void SaveResultSuccess()
        {
            var result = _subject.SaveOne(_ag1);
            Assert.That(result, Is.EqualTo(StorageResult.Success));
        }

        [Test]
        public void SaveResultAllreadyExcists()
        {
            _subject.SaveOne(_ag1);
            var result = _subject.SaveOne(_ag2);
            Assert.That(result, Is.EqualTo(StorageResult.AllreadyExsists));
        }

        [Ignore]
        [Test]
        public void SaveResultFailed()
        {
            var result = _subject.SaveOne(_ag1);
            Assert.That(result, Is.EqualTo(StorageResult.Failed));
        }

        [Test]
        public void UpdateResultSuccess()
        {
            _subject.SaveOne(_ag1);
            _ag1.Name = "TestingTesting";
            var result = _subject.UpdateOne(_ag1);
            Assert.That(result, Is.EqualTo(StorageResult.Success));
        }

        [Ignore]
        [Test]
        public void UpdateResultFailed()
        {
            var result = _subject.UpdateOne(_ag1);
            Assert.That(result, Is.EqualTo(StorageResult.Failed));
        }

        [Test]
        public  void DeleteResultSuccess()
        {
            _subject.SaveOne(_ag1);
            var result = _subject.DeleteOne(_ag1);
            Assert.That(result, Is.EqualTo(StorageResult.Success));
        }

        [Ignore]
        [Test]
        public void DeleteResultFailed()
        {
            _subject.SaveOne(_ag1);
            var result = _subject.DeleteOne(_ag1);
            Assert.That(result, Is.EqualTo(StorageResult.Success));
        }

        [Test]
        public void CanGetOneSuccess()
        {
            _subject.SaveOne(_ag1);
            _subject.SaveOne(_ag3);
            var res1 = _subject.GetOne(_ag1.Id);
            Assert.That(res1.Id, Is.EqualTo(_ag1.Id));

        }

        [Ignore]
        [Test]
        public void CanHandleFailWhenFailingGetOne()
        {
            _subject.SaveOne(_ag1);
            _subject.SaveOne(_ag3);
            var res1 = _subject.GetOne(_ag1.Id);
            Assert.That(res1, Is.EqualTo(StorageResult.Failed));
        }

        [Test]
        public void CanGetAllAccessGroups()
        {
            _subject.SaveOne(_ag1);
            _subject.SaveOne(_ag3);
            var res1 = _subject.GetOne(_ag1.Id);
            var res2 = _subject.GetOne(_ag1.Id);
            var resAll = _subject.GetAll();
            Assert.That(IsInCollection(res1, resAll));
            Assert.That(IsInCollection(res2, resAll));
        }

        [Ignore]
        [Test]
        public void CanHandleFailesGetAll()
        {
            //Test Exception
        }

        [TearDown]
        public void TearDown()
        {
            _subject.DeleteOne(_ag1);
            _subject.DeleteOne(_ag2);
            _subject.DeleteOne(_ag3);
        }

        private static bool IsInCollection(AccessGroupDTO u, IEnumerable<AccessGroupDTO> fromDb)
        {
            foreach (var d in fromDb)
            {
                if (d.Id == u.Id && d.Name == u.Name && d.Type == u.Type && d.Description == u.Description && d.RolesThatHaveAccess == u.RolesThatHaveAccess)
                    return true;
            }
            return false;
        }
    }
}
