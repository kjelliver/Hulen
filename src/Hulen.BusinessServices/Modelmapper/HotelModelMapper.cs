using System;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Modelmapper
{
    public class HotelModelMapper : IHotelModelMapper
    {
        public HotelDTO ToDTO(Hotel model)
        {
            try
            {
                return new HotelDTO
                           {
                               Id = model.Id,
                               Name = model.Name,
                               SingleRoomPrice = model.SingleRoomPrice,
                               DoubleRoomPrice = model.DoubleRoomPrice,
                               TripleRoomPrice = model.TripleRoomPrice,
                               GroupRoomPrice = model.GroupRoomPrice,
                               ExtraBedPrice = model.ExtraBedPrice,
                               IsActive = model.IsActive
                           };
            }
            catch (Exception)
            {
                throw new Exception("Error occured when mapping hotel service model to DTO: " + model.Name);
            }
        }

        public Hotel FromDTO(HotelDTO dto)
        {
            try
            {
                return new Hotel
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    SingleRoomPrice = dto.SingleRoomPrice,
                    DoubleRoomPrice = dto.DoubleRoomPrice,
                    TripleRoomPrice = dto.TripleRoomPrice,
                    GroupRoomPrice = dto.GroupRoomPrice,
                    ExtraBedPrice = dto.ExtraBedPrice,
                    IsActive = dto.IsActive
                };
            }
            catch (Exception)
            {
                throw new Exception("Error occured when mapping DTO to hotel service model: " + dto.Name);
            }
        }
    }
}
