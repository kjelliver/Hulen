using System.Collections.Generic;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;

namespace Hulen.Storage.Repositories
{
    public class AccountInfoResultsRepository : IAccountInfoResultsRepository
    {
        public ICollection<AccountInfoResultsDTO> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(AccountInfoResultsDTO)).List<AccountInfoResultsDTO>();
            }
        }
    }
}
