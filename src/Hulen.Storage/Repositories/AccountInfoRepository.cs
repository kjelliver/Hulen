using System;
using System.Collections.Generic;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Hulen.Storage.Repositories
{
    public class AccountInfoRepository : IAccountInfoRepository
    {
        public void SaveOne(AccountInfoDTO accountCategory)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(accountCategory);
                transaction.Commit();
            }
        }

        public void SaveMeny(ICollection<AccountInfoDTO> accounts)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (AccountInfoDTO acc in accounts)
                {
                    session.Save(acc);
                }
                transaction.Commit();
            }
        }

        public AccountInfoDTO GetOneById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<AccountInfoDTO>(id);
        }

        public ICollection<AccountInfoDTO> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(AccountInfoDTO)).AddOrder(Order.Asc("AccountNumber")).List<AccountInfoDTO>();
            }
        }

        public AccountInfoDTO GetOneByAccountNumber(int accountNumber)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<AccountInfoDTO>(accountNumber);
        }

        public void UpdateOne(AccountInfoDTO accountCategory)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(accountCategory);
                transaction.Commit();
            }
        }

        public void DeleteOne(AccountInfoDTO accountCategory)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(accountCategory);
                transaction.Commit();
            }
        }

        public void DeleteExistingAccountInfo()
        {
            var temp = GetAll();

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
    }
}
