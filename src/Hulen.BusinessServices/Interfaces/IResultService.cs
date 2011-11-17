﻿using System;
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
        void DeleteResultByMonth(string period, int year);
        IEnumerable<ResultAccountDTO> TryToImportFile(Stream inputStream, string month, string year, string comment, string usedBudget);

        ResultAccountDTO GetOneResultAccountById(Guid id);
        ResultAccountDTO GetOneByAccountNumberMonthAndYear(string accountNumber, string month, string year);
        void SaveMenyResultAccounts(List<ResultAccountDTO> resultAccounts);
        IEnumerable<ResultAccountDTO> GetAllResultAccountsByYearAndPeriod(int year, string period);

        IEnumerable<Result> NewGetOverviewByYear(int year);
        //IEnumerable<ResultDTO> GetOverviewByYear(int year);

        Result NewGetOneResultByYearAndStatus(string period, int year);
        //ResultDTO GetOneResultByYearAndStatus(int year, string period);

    }
}
