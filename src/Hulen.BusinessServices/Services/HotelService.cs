using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.Interfaces;

namespace Hulen.BusinessServices.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelModelMapper _hotelModelMapper;
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelModelMapper hotelModelMapper, IHotelRepository hotelRepository)
        {
            _hotelModelMapper = hotelModelMapper;
            _hotelRepository = hotelRepository;
        }

        public void SaveNewHotel(Hotel hotel)
        {
            _hotelRepository.SaveOne(_hotelModelMapper.ToDTO(hotel));
        }

        public Hotel GetOneHotelById(int id)
        {
            return _hotelModelMapper.FromDTO(_hotelRepository.GetOne(id));
        }

        public void UpdateHotel(Hotel hotel)
        {
            _hotelRepository.UpdateOne(_hotelModelMapper.ToDTO(hotel));
        }

        public void DeleteHotel(Hotel hotel)
        {
            _hotelRepository.DeleteOne(_hotelModelMapper.ToDTO(hotel));
        }

        public IEnumerable<Hotel> GetAllHotels()
        {
            var dtos = _hotelRepository.GetAll();
            return dtos.Select(dto => _hotelModelMapper.FromDTO(dto)).ToList();
        }
    }
}
