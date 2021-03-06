﻿using System;
using System.Collections.Generic;
using Hulen.Storage.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IResultRepository
    {
        IEnumerable<ResultDTO> GetOverview();
        void DeleteExcistingOverview(int period, int year);
        void DeleteExcistingAccounts(int period, int year);
        void SaveNewOverview(ResultDTO resultDTO);
        ResultDTO GetOverviewByPeriodAndYear(int period, int year);
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
