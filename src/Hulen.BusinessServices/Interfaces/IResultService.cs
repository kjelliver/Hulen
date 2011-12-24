using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.ServiceModel;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IResultService
    {
        void DeleteResultByMonth(string period, int year);
        IEnumerable<ResultAccount> TryToImportFile(Stream inputStream, string month, string year, string comment, string usedBudget);

        ResultAccount GetOneResultAccountById(Guid id);
        ResultAccount GetOneByAccountNumberMonthAndYear(string accountNumber, string month, string year);
        void SaveMenyResultAccounts(List<ResultAccount> resultAccounts);
        IEnumerable<ResultAccount> GetAllResultAccountsByYearAndPeriod(int year, string period);

        IEnumerable<Result> GetOverviewByYear(int year);
        Result GetOneResultByYearAndStatus(string period, int year);
    }
}
