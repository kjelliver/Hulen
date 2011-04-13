using System.IO;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IFileImportService
    {
        void ImportFile(string content, Stream inputStream, string year);
        void ImportFile(string content, Stream inputStream, string year, string budgetStatus);

    }
}
