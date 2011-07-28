using System.Collections.Generic;
using System.Linq;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.Storage
{   
    [TestFixture]
    public class MenuRepositoryTests
    {
        private IMenuRepository _menuRepository;

        [SetUp]
        public void SetUp()
        {
            _menuRepository = new MenuRepository();
        }

        [Test]
        public void CanGetAllMenuItems()
        {
            IEnumerable<MenuItemDTO> menuItems = _menuRepository.GetAllItems();
            Assert.Greater(menuItems.Count(), 0);
        }

        //[Test]
        //public void AddMenuItem()
        //{
        //    var menuitem = new MenuItemDTO
        //                       {
        //                           Action = "Index",
        //                           Controller = "AccessGroup",
        //                           IsLink = true,
        //                           MenuLevel = 2,
        //                           Name = "Admin. tilgangsgrupper",
        //                           Parent = "Admin",
        //                           SortOrder = 105
        //                       };
        //    _menuRepository.SaveOne(menuitem);
        //}
    }
}


