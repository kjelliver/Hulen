using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Modelmapper
{
    public class FixedArrangementCostsModelMapper : IFixedArrangementCostsModelMapper
    {
        public FixedArrangementCostsDTO ToDTO(FixedArrangementCosts serviceModel )
        {
            return new FixedArrangementCostsDTO
                       {
                           Id = serviceModel.Id,
                           GeneratedDate = serviceModel.GeneratedDate,
                           PricePerBeer = serviceModel.PricePerBeer,
                           PricePerWine = serviceModel.PricePerWine,
                           FixedTechRental = serviceModel.FixedTechRental,
                           SoundmanSalery = serviceModel.SoundmanSalery,
                           SoundmanSaleryPerWarmUp = serviceModel.SoundmanSaleryPerWarmUp,
                           PromotionExpences = serviceModel.PromotionExpences,
                           FixedCosts = serviceModel.FixedCosts,
                           DocumentId = serviceModel.DocumentId
                       };
        }

        public FixedArrangementCosts ToServiceModel(FixedArrangementCostsDTO dto)
        {
            return new FixedArrangementCosts
            {
                Id = dto.Id,
                GeneratedDate = dto.GeneratedDate,
                PricePerBeer = dto.PricePerBeer,
                PricePerWine = dto.PricePerWine,
                FixedTechRental = dto.FixedTechRental,
                SoundmanSalery = dto.SoundmanSalery,
                SoundmanSaleryPerWarmUp = dto.SoundmanSaleryPerWarmUp,
                PromotionExpences = dto.PromotionExpences,
                FixedCosts = dto.FixedCosts,
                DocumentId = dto.DocumentId
            };
        }
    }
}
