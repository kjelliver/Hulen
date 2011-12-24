using System.Collections.Generic;
using Hulen.Storage.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IBudgetRepository
    {
        IEnumerable<BudgetDTO> GetOverview();
        void SaveOneOverView(BudgetDTO budgetOverview);
        void DeleteExistingBudgetBudgetOverview(int year, string budgetStatus);
        BudgetDTO GetOverviewByYearAndStatus(int year, string budgetStatus);


        void Add(IEnumerable<BudgetAccountDTO> budgets);
        void DeleteExistingBudgetByYearAndStatus(int year, int status);
        IEnumerable<BudgetAccountDTO> GetBudgetAccountsByYearAndStatus(int year, int budgetStatus);
    }
}
