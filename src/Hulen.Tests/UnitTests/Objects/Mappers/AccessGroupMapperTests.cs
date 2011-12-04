using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.Mappers;
using Hulen.Objects.Models;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.Objects.Mappers
{
    [TestFixture]
    public class AccessGroupMapperTests
    {
        private MapAccessGroup _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new MapAccessGroup();
        }

        [Test]
        public void CanMapToDTOWithOneRole()
        {
            var viewModel = new AccessGroup
                                {
                                    Id = new Guid(),
                                    Name = "AccessTest",
                                    Type = "TestGroup",
                                    Description ="Bare en test",
                                    RolesThatHaveAccess = new List<string> {"Administrator"}
                                };
            var result = _subject.ToDTO(viewModel);
            Assert.That(result.Id, Is.EqualTo(viewModel.Id));
            Assert.That(result.Name, Is.EqualTo(viewModel.Name));
            Assert.That(result.Type, Is.EqualTo(viewModel.Type));
            Assert.That(result.Description, Is.EqualTo(viewModel.Description));
            Assert.That(result.RolesThatHaveAccess, Is.EqualTo("0"));
        }

        [Test]
        public void CanMapToDTOWithSeveralRoles()
        {
            var viewModel = new AccessGroup
            {
                Id = new Guid(),
                Name = "AccessTest",
                Type = "TestGroup",
                Description = "Bare  en test",
                RolesThatHaveAccess = new List<string> { "Administrator", "Leder" }
            };
            var result = _subject.ToDTO(viewModel);
            Assert.That(result.Id, Is.EqualTo(viewModel.Id));
            Assert.That(result.Name, Is.EqualTo(viewModel.Name));
            Assert.That(result.Type, Is.EqualTo(viewModel.Type));
            Assert.That(result.Description, Is.EqualTo(viewModel.Description));
            Assert.That(result.RolesThatHaveAccess, Is.EqualTo("0,1"));
        }

        [Test]
        public void CanMapToViewModelWithOneRole()
        {
            var dto = new AccessGroupDTO
                          {
                              Id = new Guid(),
                              Name = "AccessTets",
                              Type = "TestGroup",
                              Description = "Bare en test",
                              RolesThatHaveAccess = "0"
                          };
            var result = _subject.ToViewModel(dto);
            Assert.That(result.Id, Is.EqualTo(dto.Id));
            Assert.That(result.Name, Is.EqualTo(dto.Name));
            Assert.That(result.Type, Is.EqualTo(dto.Type));
            Assert.That(result.Description, Is.EqualTo(dto.Description));
            Assert.That(result.RolesThatHaveAccess.First(), Is.EqualTo("Administrator"));
        }

        [Test]
        public void CanMapToViewModelWithSeveralRoles()
        {
            var dto = new AccessGroupDTO
            {
                Id = new Guid(),
                Name = "AccessTets",
                Type = "TestGroup",
                Description = "Bare en test",
                RolesThatHaveAccess = "0,1"
            };
            var result = _subject.ToViewModel(dto);
            Assert.That(result.Id, Is.EqualTo(dto.Id));
            Assert.That(result.Name, Is.EqualTo(dto.Name));
            Assert.That(result.Type, Is.EqualTo(dto.Type));
            Assert.That(result.Description, Is.EqualTo(dto.Description));
            Assert.That(result.RolesThatHaveAccess.First(), Is.EqualTo("Administrator"));
            Assert.That(result.RolesThatHaveAccess[1], Is.EqualTo("Leder"));

        }
    }
}
