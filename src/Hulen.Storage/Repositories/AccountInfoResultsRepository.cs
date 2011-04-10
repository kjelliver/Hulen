using System.Collections.Generic;
using System.Linq;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Hulen.Storage.Repositories
{
    public class AccountInfoResultsRepository : IAccountInfoResultsRepository
    {
        public IEnumerable<AccountInfoResultsDTO> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(AccountInfoResultsDTO)).AddOrder(Order.Asc("Id")).List<AccountInfoResultsDTO>();
            }
        }
    }
}
