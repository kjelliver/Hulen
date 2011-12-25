using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Storage.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IHotelRepository
    {
        void SaveOne(HotelDTO dto);
        HotelDTO GetOne(int id);
        void UpdateOne(HotelDTO dto);
        void DeleteOne(HotelDTO dto);
        IEnumerable<HotelDTO> GetAll();
    }
}
