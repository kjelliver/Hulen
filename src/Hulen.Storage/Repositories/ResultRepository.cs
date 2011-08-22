using System;
using System.Collections;
using System.Collections.Generic;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Hulen.Storage.Repositories
{
    public class ResultRepository :IResultRepository
    {
        public IEnumerable<ResultDTO> GetOverview()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session
                    .CreateCriteria(typeof(ResultDTO))
                    .List<ResultDTO>();
            }
        }

        public void DeleteExcistingOverview(string period, int year)
        {
            var resultToDelete = GetOverviewByPeriodAndYear(year, period);

            if (resultToDelete != null)
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(resultToDelete);
                    transaction.Commit();
                }
            }
        }

        public void DeleteExcistingAccounts(int period, int year)
        {
            var existingResult = GetResultByMonth(period, year);

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

        public void SaveNewOverview(ResultDTO resultDTO)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(resultDTO);
                transaction.Commit();
            }
        }

        public ResultDTO GetOverviewByPeriodAndYear(int year, string period)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var results = session
                    .CreateCriteria(typeof(ResultDTO))
                    .Add(Restrictions.Eq("Year", year))
                    .Add(Restrictions.Eq("Period", period))
                    .List<ResultDTO>();
                if (results.Count < 1)
                    return null;
                return results[0];
            } 
        }


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
