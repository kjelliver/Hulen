using System;
using System.Collections;
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

        public ResultAccountDTO GetOneResultAccountById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<ResultAccountDTO>(id);
        }

        public ResultAccountDTO GetOneByAccountNumberMonthAndYear(int accountNumber, int month, int year)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var result = session.CreateCriteria(typeof (ResultAccountDTO))
                                                           .Add(Restrictions.Eq("AccountNumber", accountNumber))
                                                           .Add(Restrictions.Eq("Month", month))
                                                           .Add(Restrictions.Eq("Year", year)).List();
                return (ResultAccountDTO) result[0];
            }
        }

        public void UpdateMeny(List<ResultAccountDTO> resultAccounts)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach(ResultAccountDTO resultAccount in resultAccounts)
                {
                    session.Update(resultAccount);                        
                }
                transaction.Commit();
            }
        }
    }
}
