using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Modelmapper.Interfaces
{
    public interface IFixedArrangementCostsModelMapper
    {
        FixedArrangementCostsDTO ToDTO(FixedArrangementCosts serviceModel);
        FixedArrangementCosts ToServiceModel(FixedArrangementCostsDTO dto);
    }
}
