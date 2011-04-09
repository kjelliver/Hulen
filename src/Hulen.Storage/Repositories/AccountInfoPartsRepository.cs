

using System;
using System.Collections.Generic;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Hulen.Storage.Repositories
{
    public class AccountInfoPartsRepository : IAccountInfoPartsRepository
    {
        public ICollection<AccountInfoPartsDTO> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(AccountInfoPartsDTO)).List<AccountInfoPartsDTO>();
            }
        }
    }
}
