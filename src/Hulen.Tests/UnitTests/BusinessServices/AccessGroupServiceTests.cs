using System;
using System.Collections.Generic;
using Hulen.BusinessServices.Services;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Objects.Mappers.Interfaces;
using Hulen.Objects.Model;
using Hulen.Storage.Interfaces;
using Moq;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.BusinessServices
{
    [TestFixture]
    public class AccessGroupServiceTests
    {
        private Mock<IAccessGroupRepository> _accessGroupRepositoryMock;
        private Mock<IMapAccessGroup> _accessGroupMapper;
        private AccessGroupService _subject;

        [SetUp]
        public void SetUp()
        {
            _accessGroupRepositoryMock = new Mock<IAccessGroupRepository>();
            _accessGroupMapper = new Mock<IMapAccessGroup>();
            _subject = new AccessGroupService(_accessGroupRepositoryMock.Object, _accessGroupMapper.Object);
        }

        [Test]
        public void GetAllAccessGroupsShouldCallRepository()
        {
            _accessGroupRepositoryMock.Setup(x => x.GetAll()).Returns(new List<AccessGroupDTO>());
            _subject.GetAllAccessGroups();
            _accessGroupRepositoryMock.Verify(x => x.GetAll(), Times.Once());
        }

        [Test]
        public void SaveOneAccessGroupShouldCallRepository()
        {
            var acc = new AccessGroupDTO();
            var accView = new AccessGroup();
            _accessGroupRepositoryMock.Setup(x => x.SaveOne(acc)).Returns(StorageResult.Success);
             var result = _subject.SaveOneAccessGroup(accView);
            Assert.That(result, Is.EqualTo(StorageResult.Success));
        }

        [Test]
        public void GetOneAccessGroupShouldCallRepository()
        {
            var id = new Guid();
            var acc = new AccessGroupDTO
                          {
                              Id = id,
                              Name = "Test"
                          };
            _accessGroupRepositoryMock.Setup(x => x.GetOne(id)).Returns(acc);
            _accessGroupMapper.Setup(x => x.ToViewModel(acc)).Returns(new AccessGroup {Id = id, Name = "Test"});
            var result = _subject.GetOneAccessGroup(id);
            Assert.That(result.Name, Is.EqualTo("Test"));
        }

        [Test]
        public void UpdateOneGroupAccessShouldCallRepository()
        {
            var acc = new AccessGroupDTO();
            var accView = new AccessGroup();
            _accessGroupRepositoryMock.Setup(x => x.UpdateOne(acc)).Returns(StorageResult.Success);
            var result = _subject.UpdateOneAccessGroup(accView);
            Assert.That(result, Is.EqualTo(StorageResult.Success));
        }

        [Test]
        public void DeleteOneAccessGroupShouldCallRepository()
        {
            var acc = new AccessGroupDTO();
            var accView = new AccessGroup();
            _accessGroupRepositoryMock.Setup(x => x.DeleteOne(acc)).Returns(StorageResult.Success);
            var result = _subject.DeleteOneAccessGroup(accView);
            Assert.That(result, Is.EqualTo(StorageResult.Success));
        }
    }
}
