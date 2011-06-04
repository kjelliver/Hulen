using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Storage;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;
using NHibernate;
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

        [Ignore]
        [Test]
        public void Can_Store_One_Menu_Item()
        {
            var menuItem = new MenuItemDTO { Name = "Brukeradmin", Action = "Index", Controller = "User" };
            _menuRepository.SaveOneItem(menuItem);

            //using (ISession session = NHibernateHelper.OpenSession())
            //{
            //    var fromDb = session.GetOne<MenuItemDTO>(menuItem.Name);
            //    // Test that the product was successfully inserted
            //    Assert.IsNotNull(fromDb);
            //    Assert.AreNotSame(product, fromDb);
            //    Assert.AreEqual(product.Name, fromDb.Name);
            //    Assert.AreEqual(product.Category, fromDb.Category);
            //}
        }

        [Ignore]
        [Test]
        public void Can_Get_All_Menu_Items()
        {
            IEnumerable<MenuItemDTO> menuItems = _menuRepository.GetAllItems();
            Assert.AreEqual(1, menuItems.Count());
        }
    }
}


