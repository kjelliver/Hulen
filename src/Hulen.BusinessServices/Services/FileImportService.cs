using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;

namespace Hulen.BusinessServices.Services
{
    public class FileImportService : IFileImportService
    {
        private readonly IAccountInfoService _accountInfoServices = new AccountInfoService();
        private readonly IBudgetService _budgetService = new BudgetService(); 

        public void ImportFile(string content, Stream inputStream, string year)
        {
            _accountInfoServices.ImportFile(inputStream, year);
        }

        public void ImportFile(string content, Stream inputStream, string year, string budgetStatus)
        {
            _budgetService.ImportFile(inputStream, year, budgetStatus);
        }
    }
}
