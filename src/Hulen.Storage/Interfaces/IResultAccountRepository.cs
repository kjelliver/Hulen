
using System.Collections.Generic;
using Hulen.Storage.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IResultAccountRepository
    {
        void Add(ICollection<ResultAccount> results);
        ICollection<ResultAccount> GetResultByMonth(int month, int year);
        ICollection<ResultAccount> GetResultByYear(int year);
        void DeleteExistingResult(ICollection<ResultAccount> existingResult);
    }
}
