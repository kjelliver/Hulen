using System;
using System.Collections.Generic;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Hulen.Storage.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        public void Add(IEnumerable<BudgetDTO> budgets)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (BudgetDTO budget in budgets)
                {
                    session.Save(budget);
                }  
                transaction.Commit();
          }
        }

        public IEnumerable<BudgetDTO> GetBudgetByYearAndStatus(int year, int status)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var budget = session
                    .CreateCriteria(typeof(BudgetDTO))
                    .Add(Restrictions.Eq("Year", year))
                    .Add(Restrictions.Eq("BudgetStatus", status))
                    .List<BudgetDTO>();
                return budget;
            }
        }

        public void DeleteExistingBudgetByYearAndStatus(int year, int status)
        {
            var budgetsToDelete = GetBudgetByYearAndStatus(year, status);

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (BudgetDTO budget in budgetsToDelete)
                {
                    session.Delete(budget);

                }
                transaction.Commit();
            }
        }
    }
}
