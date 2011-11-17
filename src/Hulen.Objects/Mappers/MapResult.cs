using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Objects.Mappers.Interfaces;
using Hulen.Objects.ViewModels;

namespace Hulen.Objects.Mappers
{
    public class MapResult : IMapResult
    {
        public ResultDTO ToDTO(Result result)
        {
            return new ResultDTO
                       {
                           Id = result.Id,
                           Period = (int) System.Enum.Parse(typeof(ResultPeriod), result.Period),
                           Year = result.Year,
                           Comment = result.Comment,
                           UsedBudget = (int) System.Enum.Parse(typeof(BudgetType), result.UsedBudget)
                       };
        }

        public Result FromDTO(ResultDTO dto)
        {
            return new Result
                       {
                           Id = dto.Id,
                           Period = ((ResultPeriod)dto.Period).ToString(),
                           Year = dto.Year,
                           Comment = dto.Comment,
                           UsedBudget = ((BudgetType)dto.UsedBudget).ToString()
                       };
        }

        public IEnumerable<Result> ManyFromDTO(IEnumerable<ResultDTO> dtos)
        {
            var result = new List<Result>();
            foreach(var resultDto in dtos)
            {
                result.Add(FromDTO(resultDto));
            }
            return result;
        }
    }
}
