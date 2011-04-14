using System;
using System.Collections.Generic;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Hulen.Storage.Repositories
{
    public class ResultAccountRepository :IResultAccountRepository
    {
        public void SaveMeny(IEnumerable<ResultAccountDTO> results)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (ResultAccountDTO result in results)
                {
                    session.Save(result);
                }
                transaction.Commit();
            }
        }

        public IEnumerable<ResultAccountDTO> GetResultByMonth(int month, int year)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var results = session
                    .CreateCriteria(typeof(ResultAccountDTO))
                    .Add(Restrictions.Eq("Month", month))
                    .Add(Restrictions.Eq("Year", year))
                    .List<ResultAccountDTO>();
                return results;
            }
        }

        public IEnumerable<ResultAccountDTO> GetResultByYear(int year)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var results = session
                    .CreateCriteria(typeof(ResultAccountDTO))
                    .Add(Restrictions.Eq("Year", year))
                    .List<ResultAccountDTO>();
                return results;
            }
        }

        public void DeleteExistingResult(IEnumerable<ResultAccountDTO> existingResult)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (ResultAccountDTO result in existingResult)
                {
                    session.Delete(result);
                }
                transaction.Commit();
            }
        }
    }
}
