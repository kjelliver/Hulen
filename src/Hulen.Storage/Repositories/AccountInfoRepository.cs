using System;
using System.Collections.Generic;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;

namespace Hulen.Storage.Repositories
{
    public class AccountInfoRepository : IAccountInfoRepository
    {
        public void Add(AccountInfo accountCategory)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(accountCategory);
                transaction.Commit();
            }
        }

        public void Update(AccountInfo accountCategory)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(accountCategory);
                transaction.Commit();
            }
        }

        public void Delete(AccountInfo accountCategory)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(accountCategory);
                transaction.Commit();
            }
        }

        public AccountInfo GetByAccountNumber(int accountNumber)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<AccountInfo>(accountNumber);
        }

        public ICollection<AccountInfo> GetAllAccountCategories()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(AccountInfo)).List<AccountInfo>();
            }
        }

        public void DeleteExistingAccountInfo()
        {
            var temp = GetAllAccountCategories();

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (AccountInfo accountInfo in temp)
                {
                    session.Delete(accountInfo);                    
                }
                transaction.Commit();
            }
        }

        public void AddMeny(ICollection<AccountInfo> accounts)
        {
            foreach(AccountInfo acc in accounts)
            {
                Add(acc);
            }
        }
    }
}
