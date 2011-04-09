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
        private readonly IAccountInfoServices _accountInfoServices = new AccountInfoServices();

        public void ImportFile(string content, Stream inputStream, string year)
        {
            _accountInfoServices.ImportFile(inputStream, year);
        }
    }
}
