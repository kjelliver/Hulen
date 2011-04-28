using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.ViewModels;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IResultAccountService
    {
        void ImportFile(Stream inputStream, string month, string year);
        List<ResultAccountDTO> TryToImportFile(Stream inputStream, string month, string year);
        ResultAccountDTO GetOneResultAccountById(Guid id);
        ResultAccountDTO GetOneByAccountNumberMonthAndYear(string accountNumber, string month, string year);
        void UpdateMenyResultAccounts(List<ResultAccountDTO> resultAccounts);
    }
}
