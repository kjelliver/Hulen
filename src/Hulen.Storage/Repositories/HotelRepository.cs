using System;
using System.Collections.Generic;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Hulen.Storage.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        public void SaveOne(HotelDTO dto)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(dto);
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                throw new HibernateException("Error when saving hotel: " + dto.Name);
            } 
        }

        public HotelDTO GetOne(int id)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    return session.Get<HotelDTO>(id);
                }
            }
            catch (Exception)
            {
               throw new HibernateException("Error when getting hotel with id: " + id);
            }
        }

        public void UpdateOne(HotelDTO dto)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(dto);
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                throw new HibernateException("Error when updating " + dto.Name);
            } 
        }

        public void DeleteOne(HotelDTO dto)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(dto);
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                throw new HibernateException("Error when deleting hotel: " + dto.Name);
            }
        }

        public IEnumerable<HotelDTO> GetAll()
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    return session
                        .CreateCriteria(typeof(HotelDTO))
                        .AddOrder(Order.Desc("IsActive"))
                        .List<HotelDTO>();
                }
            }
            catch (Exception)
            {
                throw new HibernateException("Error when getting all hotels.");
            }
        }
    }
}
