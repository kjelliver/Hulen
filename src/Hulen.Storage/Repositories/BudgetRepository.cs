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
        public IEnumerable<BudgetDTO> GetOverview()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var budget = session
                    .CreateCriteria(typeof(BudgetDTO))
                    .List<BudgetDTO>();
                return budget;
            }
        }

        public void SaveOneOverView(BudgetDTO budgetOverview)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(budgetOverview);
                transaction.Commit();
            }
        }

        public void DeleteExistingBudgetBudgetOverview(int year, string budgetStatus)
        {
            var budgetToDelete = GetOverviewByYearAndStatus(year, budgetStatus);

            if (budgetToDelete != null)
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(budgetToDelete);
                    transaction.Commit();
                }
            }
        }

        public BudgetDTO GetOverviewByYearAndStatus(int year, string budgetStatus)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var budgets =  session
                    .CreateCriteria(typeof (BudgetDTO))
                    .Add(Restrictions.Eq("Year", year))
                    .Add(Restrictions.Eq("BudgetStatus", budgetStatus))
                    .List<BudgetDTO>();
                if (budgets.Count < 1)
                    return null;
                return budgets[0];
            } 
        }

        public void Add(IEnumerable<BudgetAccountDTO> budgets)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (BudgetAccountDTO budget in budgets)
                {
                    session.Save(budget);
                }  
                transaction.Commit();
            }
        }

        public void DeleteExistingBudgetByYearAndStatus(int year, int status)
        {
            var budgetsToDelete = GetBudgetAccountsByYearAndStatus(year, status);

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (BudgetAccountDTO budget in budgetsToDelete)
                {
                    session.Delete(budget);

                }
                transaction.Commit();
            }
        }

        public IEnumerable<BudgetAccountDTO> GetBudgetAccountsByYearAndStatus(int year, int budgetStatus)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var budget = session
                    .CreateCriteria(typeof(BudgetAccountDTO))
                    .Add(Restrictions.Eq("Year", year))
                    .Add(Restrictions.Eq("BudgetStatus", budgetStatus))
                    .List<BudgetAccountDTO>();
                return budget;
            }
        }
    }
}
