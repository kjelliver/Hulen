using System;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;

namespace Hulen.Storage.Repositories
{
    public class FixedArrangementCostsRepository : IFixedArrangementCostsRepository
    {
        public FixedArrangementCostsDTO GetOne()
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    return session.Get<FixedArrangementCostsDTO>(1);
                }
            }
            catch (Exception)
            {
                throw new HibernateException("Error when getting fixed arrangement costs");
            }
        }

        public void UpdateOne(FixedArrangementCostsDTO dto)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(dto);
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                throw new HibernateException("Error when updating fixed arrangement costs");
            }  
        }
    }
}
