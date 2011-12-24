using System.Collections.Generic;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Modelmapper.Interfaces
{
    public interface IResultModelMapper
    {
        ResultDTO ToDTO(Result result);
        Result FromDTO(ResultDTO dto);
        IEnumerable<Result> ManyFromDTO(IEnumerable<ResultDTO> dtos);
    }
}
