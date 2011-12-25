using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Modelmapper;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.BusinessServices.Mappers
{
    [TestFixture]
    public class HotelModelMapperTests
    {
        private HotelModelMapper _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new HotelModelMapper();
        }

        [Test]
        public void CanMapToDTO()
        {
            var model = new Hotel{Id = 1, Name="Test", SingleRoomPrice = 100, DoubleRoomPrice = 200, TripleRoomPrice = 300, GroupRoomPrice = 400, ExtraBedPrice = 500, IsActive = true};
            var result = _subject.ToDTO(model);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Test"));
            Assert.That(result.SingleRoomPrice, Is.EqualTo(100));
            Assert.That(result.DoubleRoomPrice, Is.EqualTo(200));
            Assert.That(result.TripleRoomPrice, Is.EqualTo(300));
            Assert.That(result.GroupRoomPrice, Is.EqualTo(400));
            Assert.That(result.ExtraBedPrice, Is.EqualTo(500));
            Assert.That(result.IsActive, Is.EqualTo(true));
        }

        [Test]
        public void MapToDTOHandlesExceptions()
        {
            
        }

        [Test]
        public void CanMapFromDTO()
        {
            var dto = new HotelDTO { Id = 1, Name = "Test", SingleRoomPrice = 100, DoubleRoomPrice = 200, TripleRoomPrice = 300, GroupRoomPrice = 400, ExtraBedPrice = 500, IsActive = true };
            var result = _subject.FromDTO(dto);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Test"));
            Assert.That(result.SingleRoomPrice, Is.EqualTo(100));
            Assert.That(result.DoubleRoomPrice, Is.EqualTo(200));
            Assert.That(result.TripleRoomPrice, Is.EqualTo(300));
            Assert.That(result.GroupRoomPrice, Is.EqualTo(400));
            Assert.That(result.ExtraBedPrice, Is.EqualTo(500));
            Assert.That(result.IsActive, Is.EqualTo(true));
        }

        [Test]
        public void MapFromDTOHandlesExceptions()
        {

        }
    }
}
