using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.ViewModels;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IResultService
    {
        IEnumerable<ResultDTO> GetOverview();
        ResultDTO GetOneResultByYearAndStatus(int year, string period);
        void DeleteResultByMonth(string period, int year);
        IEnumerable<ResultAccountDTO> TryToImportFile(Stream inputStream, string month, string year, string comment);

        ResultAccountDTO GetOneResultAccountById(Guid id);
        ResultAccountDTO GetOneByAccountNumberMonthAndYear(string accountNumber, string month, string year);
        void SaveMenyResultAccounts(List<ResultAccountDTO> resultAccounts);
    }
}
