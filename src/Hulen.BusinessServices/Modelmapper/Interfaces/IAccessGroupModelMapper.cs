using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Modelmapper.Interfaces
{
    public interface IAccessGroupModelMapper
    {
        AccessGroupDTO ToDTO(AccessGroup model);
        AccessGroup ToViewModel(AccessGroupDTO dto);
    }
}
