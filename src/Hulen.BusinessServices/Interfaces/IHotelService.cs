using System.Collections.Generic;
using Hulen.BusinessServices.ServiceModel;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IHotelService
    {
        void SaveNewHotel(Hotel hotel);
        Hotel GetOneHotelById(int id);
        void UpdateHotel(Hotel hotel);
        void DeleteHotel(Hotel hotel);
        IEnumerable<Hotel> GetAllHotels();
    }
}
