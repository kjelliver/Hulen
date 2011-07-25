using System;
using System.Collections.Generic;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Storage.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Hulen.Storage.Repositories
{
    public class AccountInfoRepository : IAccountInfoRepository
    {
        public StorageResult SaveOne(AccountInfoDTO accountCategory)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(accountCategory);
                    transaction.Commit();
                }
                return StorageResult.Success;
            }
            catch (Exception)
            {
                return StorageResult.Failed;
            }            
        }

        public IEnumerable<AccountInfoDTO> GetAllByYear(int year)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session
                    .CreateCriteria(typeof(AccountInfoDTO))
                    .AddOrder(Order.Asc("AccountNumber"))
                    .Add(Restrictions.Eq("Year", year))
                    .List<AccountInfoDTO>();
            }
        }

        public StorageResult DeleteOne(AccountInfoDTO accountCategory)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(accountCategory);
                    transaction.Commit();
                }
                return StorageResult.Success;
            }
            catch (Exception)
            {
                return StorageResult.Failed;
            }
        }

    //    public void SaveMeny(ICollection<AccountInfoDTO> accounts)
    //    {
    //        using (ISession session = NHibernateHelper.OpenSession())
    //        using (ITransaction transaction = session.BeginTransaction())
    //        {
    //            foreach (AccountInfoDTO acc in accounts)
    //            {
    //                session.Save(acc);
    //            }
    //            transaction.Commit();
    //        }
    //    }

    //    public AccountInfoDTO GetOneById(Guid id)
    //    {
    //        using (ISession session = NHibernateHelper.OpenSession())
    //            return session.Get<AccountInfoDTO>(id);
    //    }

    //    public ICollection<AccountInfoDTO> GetAll()
    //    {
    //        using (ISession session = NHibernateHelper.OpenSession())
    //        {
    //            return session.CreateCriteria(typeof(AccountInfoDTO)).AddOrder(Order.Asc("AccountNumber")).List<AccountInfoDTO>();
    //        }
    //    }

    //    public IEnumerable<AccountInfoDTO> GetAllByYear(int year)
    //    {
    //        using (ISession session = NHibernateHelper.OpenSession())
    //        {
    //            return session
    //                .CreateCriteria(typeof(AccountInfoDTO))
    //                .AddOrder(Order.Asc("AccountNumber"))
    //                .Add(Restrictions.Eq("Year", year))
    //                .List<AccountInfoDTO>();
    //        }
    //    }

    //    public AccountInfoDTO GetOneByAccountNumber(int accountNumber)
    //    {
    //        using (ISession session = NHibernateHelper.OpenSession())
    //            return session.Get<AccountInfoDTO>(accountNumber);
    //    }

    //    public void UpdateOne(AccountInfoDTO accountCategory)
    //    {
    //        using (ISession session = NHibernateHelper.OpenSession())
    //        using (ITransaction transaction = session.BeginTransaction())
    //        {
    //            session.Update(accountCategory);
    //            transaction.Commit();
    //        }
    //    }

    

    //    public void DeleteExistingAccountInfo()
    //    {
    //        var temp = GetAll();

    //        using (ISession session = NHibernateHelper.OpenSession())
    //        using (ITransaction transaction = session.BeginTransaction())
    //        {
    //            foreach (AccountInfoDTO accountInfo in temp)
    //            {
    //                session.Delete(accountInfo);                    
    //            }
    //            transaction.Commit();
    //        }
    //    }

   
    }
}
