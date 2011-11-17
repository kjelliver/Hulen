using System;
using System.Collections.Generic;
using Hulen.Objects.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IResultRepository
    {
        IEnumerable<ResultDTO> GetOverview();
        void DeleteExcistingOverview(string period, int year);
        void DeleteExcistingAccounts(int period, int year);
        void SaveNewOverview(ResultDTO resultDTO);
        ResultDTO GetOverviewByPeriodAndYear(int year, string period);
        IEnumerable<ResultDTO> GetOverviewByYear(int year);



        void SaveMeny(IEnumerable<ResultAccountDTO> results);
        IEnumerable<ResultAccountDTO> GetResultByMonth(int month, int year);
        IEnumerable<ResultAccountDTO> GetResultByYear(int year);
        void DeleteExistingResult(IEnumerable<ResultAccountDTO> existingResult);
        ResultAccountDTO GetOneResultAccountById(Guid id);
        ResultAccountDTO GetOneByAccountNumberMonthAndYear(int accountNumber, int month, int year);
        void UpdateMeny(List<ResultAccountDTO> resultAccounts);
    }
}
