using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.Models;

namespace Hulen.Objects.Mappers.Interfaces
{
    public interface IMapResult
    {
        ResultDTO ToDTO(Result result);
        Result FromDTO(ResultDTO dto);
        IEnumerable<Result> ManyFromDTO(IEnumerable<ResultDTO> dtos);
    }
}
