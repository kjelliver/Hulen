using System.Collections.Generic;
using System.IO;
using Hulen.Objects.ViewModels;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IBudgetService
    {
        IEnumerable<BudgetOverviewViewModel> GetOverviewAllStoredBudgets();




        void DeleteAllBudgetsByYearAndStatus(int year, string budgetStatus);
        void ImportFile(Stream inputStream, string year, string budgetStatus);
    }
}
