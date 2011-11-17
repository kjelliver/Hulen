using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.Mappers.Interfaces;
using Hulen.Objects.ViewModels;

namespace Hulen.Objects.Mappers
{
    public class MapResult : IMapResult
    {
        public ResultDTO ToDTO(Result result)
        {
            throw new NotImplementedException();
        }

        public Result FromDTO(ResultDTO dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Result> ManyFromDTO(IEnumerable<ResultDTO> dtos)
        {
            throw new NotImplementedException();
        }
    }
}
