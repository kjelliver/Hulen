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
        void DeleteResultByYearAndStatus(int year, string period);
        IEnumerable<ResultAccountDTO> TryToImportFile(Stream inputStream, string month, string year, string comment);




        //void ImportFile(Stream inputStream, string month, string year, string comment);
        ResultAccountDTO GetOneResultAccountById(Guid id);
        ResultAccountDTO GetOneByAccountNumberMonthAndYear(string accountNumber, string month, string year);
        void UpdateMenyResultAccounts(List<ResultAccountDTO> resultAccounts);
    }
}
