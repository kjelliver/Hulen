using System;
using System.Collections.Generic;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;

namespace Hulen.Storage.Repositories
{
    public class AccountInfoRepository : IAccountInfoRepository
    {
        public void Add(AccountInfoDTO accountCategory)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(accountCategory);
                transaction.Commit();
            }
        }

        public void Update(AccountInfoDTO accountCategory)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(accountCategory);
                transaction.Commit();
            }
        }

        public void Delete(AccountInfoDTO accountCategory)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(accountCategory);
                transaction.Commit();
            }
        }

        public AccountInfoDTO GetByAccountNumber(int accountNumber)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<AccountInfoDTO>(accountNumber);
        }

        public ICollection<AccountInfoDTO> GetAllAccountCategories()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(AccountInfoDTO)).List<AccountInfoDTO>();
            }
        }

        public void DeleteExistingAccountInfo()
        {
            var temp = GetAllAccountCategories();

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (AccountInfoDTO accountInfo in temp)
                {
                    session.Delete(accountInfo);                    
                }
                transaction.Commit();
            }
        }

        public void AddMeny(ICollection<AccountInfoDTO> accounts)
        {
            foreach(AccountInfoDTO acc in accounts)
            {
                Add(acc);
            }
        }

        public AccountInfoDTO GetById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<AccountInfoDTO>(id);
        }
    }
}
