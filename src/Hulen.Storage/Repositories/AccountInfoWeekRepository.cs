using System;
using System.Collections.Generic;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Hulen.Storage.Repositories
{
    public class AccountInfoWeekRepository : IAccountInfoWeekRepository
    {
        public IEnumerable<AccountInfoWeekDTO> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(AccountInfoWeekDTO)).AddOrder(Order.Asc("Id")).List<AccountInfoWeekDTO>();
            }
        }
    }
}
