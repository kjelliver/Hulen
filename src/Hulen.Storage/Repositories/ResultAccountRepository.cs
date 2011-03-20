using System;
using System.Collections.Generic;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Hulen.Storage.Repositories
{
    public class ResultAccountRepository :IResultAccountRepository
    {
        public void Add(ICollection<ResultAccount> results)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (ResultAccount result in results)
                {
                    session.Save(result);
                }
                transaction.Commit();
            }
        }

        public ICollection<ResultAccount> GetResultByMonth(int month, int year)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var results = session
                    .CreateCriteria(typeof(ResultAccount))
                    .Add(Restrictions.Eq("Month", month))
                    .Add(Restrictions.Eq("Year", year))
                    .List<ResultAccount>();
                return results;
            }
        }

        public ICollection<ResultAccount> GetResultByYear(int year)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var results = session
                    .CreateCriteria(typeof(ResultAccount))
                    .Add(Restrictions.Eq("Year", year))
                    .List<ResultAccount>();
                return results;
            }
        }

        public void DeleteExistingResult(ICollection<ResultAccount> existingResult)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (ResultAccount result in existingResult)
                {
                    session.Delete(result);
                }
                transaction.Commit();
            }
        }
    }
}
