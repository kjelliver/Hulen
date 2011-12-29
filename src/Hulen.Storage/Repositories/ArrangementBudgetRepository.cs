using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;

namespace Hulen.Storage.Repositories
{
    public class ArrangementBudgetRepository : IArrangementBudgetRepository
    {
        public void SaveOne(ArrangementBudgetDTO dto)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(dto);
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                throw new HibernateException("Error when saving budget for " + dto.Artist );
            } 
        }

        public ArrangementBudgetDTO GetOne(int id)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    return session.Get<ArrangementBudgetDTO>(id);
                }
            }
            catch (Exception)
            {
               throw new HibernateException("Error when getting budget with id: " + id);
            }
        }

        public void UpdateOne(ArrangementBudgetDTO dto)
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
                throw new HibernateException("Error when updating budget for" + dto.Artist);
            } 
        }

        public void DeleteOne(ArrangementBudgetDTO dto)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(dto);
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                throw new HibernateException("Error when deleting budget for " + dto.Artist);
            }
        }

        public IEnumerable<ArrangementBudgetDTO> GetManyByTimeSpan(DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArrangementBudgetDTO> GetManyByTimeSpanAndStatus(DateTime fromDate, DateTime toDate, int status)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArrangementBudgetDTO> GetManyByTimeSpanAndStatusSpan(DateTime fromDate, DateTime toDate, int fromStatus, int toStatus)
        {
            throw new NotImplementedException();
        }
    }
}
