using System.Collections.Generic;
using Hulen.Objects.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IBudgetRepository
    {
        void Add(IEnumerable<BudgetDTO> budgets);
        IEnumerable<BudgetDTO> GetBudgetByYearAndStatus(int year, int status);
        void DeleteExistingBudgetByYearAndStatus(int year, int status);
    }
}
