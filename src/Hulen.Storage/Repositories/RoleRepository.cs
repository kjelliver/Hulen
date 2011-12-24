using System;
using System.Collections.Generic;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Utils.Enum;
using NHibernate;
using NHibernate.Criterion;

namespace Hulen.Storage.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public IEnumerable<RoleDTO> GetAll()
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    return session
                        .CreateCriteria(typeof(RoleDTO))
                        .AddOrder(Order.Asc("Id"))
                        .List<RoleDTO>();
                }
            }
            catch (Exception)
            {
                return new List<RoleDTO>();
            }
        }

        public StorageResult SaveOne(RoleDTO role)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(role);
                    transaction.Commit();
                }
                return StorageResult.Success;
            }
            catch (Exception)
            {
                return StorageResult.Failed;
            }  
        }

        public StorageResult UpdateOne(RoleDTO role)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(role);
                    transaction.Commit();
                }
                return StorageResult.Success;
            }
            catch (Exception)
            {
                return StorageResult.Failed;
            }  
        }

        public RoleDTO GetOne(Guid id)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    return session.Get<RoleDTO>(id);
                }
            }
            catch (Exception)
            {
                return new RoleDTO();
            }
        }
    }
}