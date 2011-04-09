using System;
using System.Collections.Generic;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;

namespace Hulen.Storage.Repositories
{
    public class AccountInfoWeekRepository : IAccountInfoWeekRepository
    {
        public ICollection<AccountInfoWeekDTO> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(AccountInfoWeekDTO)).List<AccountInfoWeekDTO>();
            }
        }
    }
}
