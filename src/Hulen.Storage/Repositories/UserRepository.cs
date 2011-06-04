using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Storage.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace Hulen.Storage.Repositories
{
    public class UserRepository : IUserRepository
    {
        public StorageResult SaveOneUser(UserDTO user)
        {
            if (GetOneUserByUsername(user.Username) == null)
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
                return StorageResult.Success;
            }
            return StorageResult.AllreadyExsists;
        }

        public UserDTO GetOneUserByUsername(string username)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session
                    .CreateCriteria(typeof(UserDTO))
                    .Add(Restrictions.Eq("Username", username))
                    .UniqueResult<UserDTO>();
            }
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(UserDTO)).AddOrder(Order.Asc("Name")).List<UserDTO>();
            }
        }

        public void DeleteOneUser(UserDTO user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(user);
                transaction.Commit();
            }
        }
    }
}
