using System.Collections.Generic;
using Hulen.Objects.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IResultAccountRepository
    {
        void SaveMeny(IEnumerable<ResultAccountDTO> results);
        IEnumerable<ResultAccountDTO> GetResultByMonth(int month, int year);
        IEnumerable<ResultAccountDTO> GetResultByYear(int year);
        void DeleteExistingResult(IEnumerable<ResultAccountDTO> existingResult);
    }
}
