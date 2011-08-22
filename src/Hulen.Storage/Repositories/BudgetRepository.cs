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
        public IEnumerable<BudgetOverviewDTO> GetOverview()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var budget = session
                    .CreateCriteria(typeof(BudgetOverviewDTO))
                    .List<BudgetOverviewDTO>();
                return budget;
            }
        }

        public void SaveOneOverView(BudgetOverviewDTO budgetOverview)
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

        public BudgetOverviewDTO GetOverviewByYearAndStatus(int year, string budgetStatus)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var budgets =  session
                    .CreateCriteria(typeof (BudgetOverviewDTO))
                    .Add(Restrictions.Eq("Year", year))
                    .Add(Restrictions.Eq("BudgetStatus", budgetStatus))
                    .List<BudgetOverviewDTO>();
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

        public IEnumerable<BudgetAccountDTO> GetBudgetByYearAndStatus(int year, int status)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var budget = session
                    .CreateCriteria(typeof(BudgetAccountDTO))
                    .Add(Restrictions.Eq("Year", year))
                    .Add(Restrictions.Eq("BudgetStatus", status))
                    .List<BudgetAccountDTO>();
                return budget;
            }
        }

        public void DeleteExistingBudgetByYearAndStatus(int year, int status)
        {
            var budgetsToDelete = GetBudgetByYearAndStatus(year, status);

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
    }
}
