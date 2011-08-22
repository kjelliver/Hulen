using System.Collections.Generic;
using Hulen.Objects.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IBudgetRepository
    {
        IEnumerable<BudgetOverviewDTO> GetOverview();
        void SaveOneOverView(BudgetOverviewDTO budgetOverview);
        void DeleteExistingBudgetBudgetOverview(int year, string budgetStatus);
        BudgetOverviewDTO GetOverviewByYearAndStatus(int year, string budgetStatus);


        void Add(IEnumerable<BudgetAccountDTO> budgets);
        IEnumerable<BudgetAccountDTO> GetBudgetByYearAndStatus(int year, int status);
        void DeleteExistingBudgetByYearAndStatus(int year, int status);
    }
}
