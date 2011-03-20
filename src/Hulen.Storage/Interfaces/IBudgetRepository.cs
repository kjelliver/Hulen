using System.Collections.Generic;
using Hulen.Storage.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IBudgetRepository
    {
        void Add(ICollection<Budget> budgets);
        ICollection<Budget> GetBudgetByYearAndStatus(int year, int status);
        void DeleteExistingBudget(ICollection<Budget> existingBudget);
    }
}
