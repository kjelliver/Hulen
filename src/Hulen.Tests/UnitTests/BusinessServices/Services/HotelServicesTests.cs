using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.BusinessServices.Services;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Moq;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.BusinessServices.Services
{
    [TestFixture]
    public class HotelServicesTests
    {
        private Mock<IHotelRepository> _repositoryMock;
        private Mock<IHotelModelMapper> _mapperMock;
        private HotelService _subject;
        private HotelDTO _dto1;
        private HotelDTO _dto2;
        private HotelDTO _dto3;
        private Hotel _model1;
        private Hotel _model2;
        private Hotel _model3;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IHotelRepository>();
            _mapperMock = new Mock<IHotelModelMapper>();
            _subject = new HotelService(_mapperMock.Object, _repositoryMock.Object);

            _dto1 = new HotelDTO {Id = 1, Name = "Testhotell 1", SingleRoomPrice = 11, DoubleRoomPrice = 12, TripleRoomPrice = 13, GroupRoomPrice = 14, ExtraBedPrice = 15, IsActive = true };
            _dto2 = new HotelDTO { Id = 2, Name = "Testhotell 2", SingleRoomPrice = 21, DoubleRoomPrice = 22, TripleRoomPrice = 23, GroupRoomPrice = 24, ExtraBedPrice = 25, IsActive = true };
            _dto3 = new HotelDTO { Id = 3, Name = "Testhotell 3", SingleRoomPrice = 31, DoubleRoomPrice = 32, TripleRoomPrice = 33, GroupRoomPrice = 34, ExtraBedPrice = 35, IsActive = true };            
            
            _model1 = new Hotel {Id = 1, Name = "Testhotell 1", SingleRoomPrice = 11, DoubleRoomPrice = 12, TripleRoomPrice = 13, GroupRoomPrice = 14, ExtraBedPrice = 15, IsActive = true };
            _model2 = new Hotel { Id = 2, Name = "Testhotell 2", SingleRoomPrice = 21, DoubleRoomPrice = 22, TripleRoomPrice = 23, GroupRoomPrice = 24, ExtraBedPrice = 25, IsActive = true };
            _model3 = new Hotel { Id = 3, Name = "Testhotell 3", SingleRoomPrice = 31, DoubleRoomPrice = 32, TripleRoomPrice = 33, GroupRoomPrice = 34, ExtraBedPrice = 35, IsActive = true };            
        }

        [Test]
        public void SaveOneHotelMapsServiceModelAndCallsRepository()
        {
            _mapperMock.Setup(x => x.ToDTO(_model1)).Returns(_dto1);

            _subject.SaveNewHotel(_model1);

            _mapperMock.Verify(x => x.ToDTO(_model1), Times.Once());
            _repositoryMock.Verify(x => x.SaveOne(_dto1), Times.Once());
        }

        [Test]
        public void GetOneHotelCallsRepositoryOnceAndMapsDTO()
        {
            _repositoryMock.Setup(x => x.GetOne(_dto1.Id)).Returns(_dto1);
            _mapperMock.Setup(x => x.FromDTO(_dto1)).Returns(_model1);

            var result = _subject.GetOneHotelById(1);

            _repositoryMock.Verify(x => x.GetOne(1), Times.Once());
            _mapperMock.Verify(x =>x.FromDTO(_dto1), Times.Once());
            Assert.That(result, Is.SameAs(_model1));
        }

        [Test]
        public void UpdateOneHotelMapsServiceModelAndCallsRepository()
        {
            _mapperMock.Setup(x => x.ToDTO(_model1)).Returns(_dto1);

            _subject.UpdateHotel(_model1);

            _mapperMock.Verify(x => x.ToDTO(_model1), Times.Once());
            _repositoryMock.Verify(x => x.UpdateOne(_dto1), Times.Once());
        }

        [Test]
        public void DeleteOneHotelMapsToServiceModelAndCallsRepository()
        {
            _mapperMock.Setup(x => x.ToDTO(_model1)).Returns(_dto1);

            _subject.DeleteHotel(_model1);

            _mapperMock.Verify(x => x.ToDTO(_model1), Times.Once());
            _repositoryMock.Verify(x => x.DeleteOne(_dto1), Times.Once());
        }

        [Test]
        public void GetAllHotelsCallsRepositoryAndMapsToServiceModels()
        {
            var dtos = new List<HotelDTO> {_dto1, _dto2, _dto3};

            _repositoryMock.Setup(x => x.GetAll()).Returns(dtos);
            _mapperMock.Setup(x => x.FromDTO(_dto1)).Returns(_model1);
            _mapperMock.Setup(x => x.FromDTO(_dto2)).Returns(_model2);
            _mapperMock.Setup(x => x.FromDTO(_dto3)).Returns(_model3);

            var result = _subject.GetAllHotels();

            _repositoryMock.Verify(x => x.GetAll(), Times.Once());
            _mapperMock.Verify(x => x.FromDTO(_dto1), Times.Once());
            _mapperMock.Verify(x => x.FromDTO(_dto2), Times.Once());
            _mapperMock.Verify(x => x.FromDTO(_dto3), Times.Once());
            Assert.That(result.ElementAt(0), Is.SameAs(_model1));
            Assert.That(result.ElementAt(1), Is.SameAs(_model2));
            Assert.That(result.ElementAt(2), Is.SameAs(_model3));
        }
    }
}
