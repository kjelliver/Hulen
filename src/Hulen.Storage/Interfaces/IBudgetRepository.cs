using System.Collections.Generic;
using Hulen.Objects.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IBudgetRepository
    {
        IEnumerable<BudgetDTO> GetOverview();
        void SaveOneOverView(BudgetDTO budgetOverview);
        void DeleteExistingBudgetBudgetOverview(int year, string budgetStatus);
        BudgetDTO GetOverviewByYearAndStatus(int year, string budgetStatus);


        void Add(IEnumerable<BudgetAccountDTO> budgets);
        IEnumerable<BudgetAccountDTO> GetBudgetByYearAndStatus(int year, int status);
        void DeleteExistingBudgetByYearAndStatus(int year, int status);
    }
}
