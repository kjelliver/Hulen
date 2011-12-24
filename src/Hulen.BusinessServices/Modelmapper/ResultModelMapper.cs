using System.Collections.Generic;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;
using Hulen.Utils.Enum;

namespace Hulen.BusinessServices.Modelmapper
{
    public class ResultModelMapper : IResultModelMapper
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
