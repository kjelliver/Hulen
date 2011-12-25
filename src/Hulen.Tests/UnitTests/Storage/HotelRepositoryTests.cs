using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Storage.DTO;
using Hulen.Storage.Repositories;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.Storage
{
    [TestFixture]
    public class HotelRepositoryTests
    {
        private HotelRepository _subject;
        private HotelDTO _testHotel1;
        private HotelDTO _testHotel2;
        private HotelDTO _testHotel3;
        [SetUp]
        public void SetUp()
        {
            _subject = new HotelRepository();
            SaveTestData();
        }

        [Test]
        public void CanSaveAndGetOne()
        {
            //If you can get this one, it meens that setup has successful saved one or more.
            var result = _subject.GetOne(_testHotel1.Id);
            Assert.That(result.Id, Is.EqualTo(_testHotel1.Id));
            Assert.That(result.Name, Is.EqualTo(_testHotel1.Name));
            Assert.That(result.SingleRoomPrice, Is.EqualTo(_testHotel1.SingleRoomPrice));
            Assert.That(result.DoubleRoomPrice, Is.EqualTo(_testHotel1.DoubleRoomPrice));
            Assert.That(result.TripleRoomPrice, Is.EqualTo(_testHotel1.TripleRoomPrice));
            Assert.That(result.GroupRoomPrice, Is.EqualTo(_testHotel1.GroupRoomPrice));
            Assert.That(result.ExtraBedPrice, Is.EqualTo(_testHotel1.ExtraBedPrice));
            Assert.That(result.IsActive, Is.EqualTo(_testHotel1.IsActive));
        }

        [Ignore]
        [Test]
        [ExpectedException(typeof(HibernateException), ExpectedMessage = "")]
        public void SaveOneHandlesExceptions()
        {
        }

        [Test]
        public void GetOneHandlesExceptions()
        {
            
        }

        [Test]
        public void CanUpdateOne()
        {
            
        }

        [Ignore]
        [Test]
        [ExpectedException(typeof(HibernateException), ExpectedMessage = "")]
        public void UpdateOneHandlesExceptions()
        {
            
        }
        
        [Test]
        public void CanDeleteOne()
        {
            
        }

        [Ignore]
        [Test]
        [ExpectedException(typeof(HibernateException), ExpectedMessage = "")]
        public void DeleteOneHandlesExceptions()
        {
            
        }

        [Test]
        public void CanGetAll()
        {
            
        }

        [Ignore]
        [Test]
        [ExpectedException(typeof(HibernateException), ExpectedMessage = "")]
        public void GetAllHandlesExceptions()
        {
            
        }

        [TearDown]
        public void TearDown()
        {
            _subject.DeleteOne(_testHotel1);
            _subject.DeleteOne(_testHotel2);
            _subject.DeleteOne(_testHotel3);
        }

        private void SaveTestData()
        {
            _testHotel1 = new HotelDTO{Name = "Testhotell 1", SingleRoomPrice = 11, DoubleRoomPrice = 12, TripleRoomPrice = 13, GroupRoomPrice = 14, ExtraBedPrice = 15, IsActive = true};
            _subject.SaveOne(_testHotel1);
            _testHotel2 = new HotelDTO { Name = "Testhotell 2", SingleRoomPrice = 21, DoubleRoomPrice = 22, TripleRoomPrice = 23, GroupRoomPrice = 24, ExtraBedPrice = 25, IsActive = true };
            _subject.SaveOne(_testHotel2);
            _testHotel3 = new HotelDTO { Name = "Testhotell 3", SingleRoomPrice = 31, DoubleRoomPrice = 32, TripleRoomPrice = 33, GroupRoomPrice = 34, ExtraBedPrice = 35, IsActive = true };
            _subject.SaveOne(_testHotel3);
        }
    }
}
