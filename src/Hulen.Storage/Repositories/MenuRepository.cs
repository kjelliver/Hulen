using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Hulen.Storage.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        public IEnumerable<MenuItemDTO> GetAllItems()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(MenuItemDTO)).AddOrder(Order.Asc("SortOrder")).List<MenuItemDTO>();
            }
        }
    }
}
