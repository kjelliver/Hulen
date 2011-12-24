using System.Collections.Generic;
using System.IO;
using Hulen.BusinessServices.ServiceModel;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IBudgetService
    {
        IEnumerable<Budget> GetOverview();
        Budget GetOneBudgetByYearAndStatus(int year, string status);

        void DeleteAllBudgetsByYearAndStatus(int year, string budgetStatus);
        void ImportFile(Stream inputStream, string year, string budgetStatus, string comment);
        IEnumerable<BudgetAccount> GetAllBudgetAccountsByYearAndStatus(int year, string budgetStatus);
    }
}
