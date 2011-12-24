using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Storage.Interfaces;
using Hulen.Utils.Enum;
using NHibernate;
using NHibernate.Criterion;
using MenuItemDTO = Hulen.Storage.DTO.MenuItemDTO;

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

        public StorageResult SaveOne(MenuItemDTO item)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(item);
                    transaction.Commit();
                }
                return StorageResult.Success;
            }
            catch (Exception)
            {
                return StorageResult.Failed;
            }
            
        }

        public MenuItemDTO GetOneById(Guid id)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    return session.Get<MenuItemDTO>(id);
                }
            }
            catch (Exception)
            {
                return new MenuItemDTO();
            }
        }

        public StorageResult UpdateOne(MenuItemDTO menuItem)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(menuItem);
                    transaction.Commit();
                }
                return StorageResult.Success;
            }
            catch (Exception)
            {
                return StorageResult.Failed;
            }  
        }

        public StorageResult DeleteOne(MenuItemDTO menuItem)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(menuItem);
                    transaction.Commit();
                }
                return StorageResult.Success;
            }
            catch (Exception)
            {
                return StorageResult.Failed;
            }
        }
    }
}
