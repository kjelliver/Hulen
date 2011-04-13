using System.IO;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IBudgetService
    {
        void DeleteAllBudgetsByYearAndStatus(int year, string budgetStatus);
        void ImportFile(Stream inputStream, string year, string budgetStatus);
    }
}
