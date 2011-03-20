using System;
using System.Collections.Generic;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Hulen.Storage.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        public void Add(ICollection<Budget> budgets)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (Budget budget in budgets)
                {
                    session.Save(budget);
                }  
                transaction.Commit();
          }
        }

        public ICollection<Budget> GetBudgetByYearAndStatus(int year, int status)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var budget = session
                    .CreateCriteria(typeof(Budget))
                    .Add(Restrictions.Eq("Year", year))
                    .Add(Restrictions.Eq("BudgetStatus", status))
                    .List<Budget>();
                return budget;
            }
        }

        public void DeleteExistingBudget(ICollection<Budget> existingBudget)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (Budget budget in existingBudget)
                {
                    session.Delete(budget);                    
                }
                transaction.Commit();
            }
        }
    }
}
