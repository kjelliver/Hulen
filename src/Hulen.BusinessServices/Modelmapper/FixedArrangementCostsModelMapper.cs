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
            var test = new FixedArrangementCosts();
            //{
            test.Id = dto.Id;
            test.GeneratedDate = dto.GeneratedDate;
            test.PricePerBeer = dto.PricePerBeer;
            test.PricePerWine = dto.PricePerWine;
            test.FixedTechRental = dto.FixedTechRental;
            test.SoundmanSalery = dto.SoundmanSalery;
            test.SoundmanSaleryPerWarmUp = dto.SoundmanSaleryPerWarmUp;
            test.PromotionExpences = dto.PromotionExpences;
            test.FixedCosts = dto.FixedCosts;
            test.DocumentId = dto.DocumentId;
            return test;
            //};
        }
    }
}
