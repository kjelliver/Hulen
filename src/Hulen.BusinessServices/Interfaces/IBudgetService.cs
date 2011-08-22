using System.Collections.Generic;
using System.IO;
using Hulen.Objects.DTO;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IBudgetService
    {
        IEnumerable<BudgetOverviewDTO> GetOverview();
        BudgetOverviewDTO GetOneBudgetByYearAndStatus(int year, string status);



        void DeleteAllBudgetsByYearAndStatus(int year, string budgetStatus);
        void ImportFile(Stream inputStream, string year, string budgetStatus, string comment);
    }
}
