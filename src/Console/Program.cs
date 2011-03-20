using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IAccountInfoRepository test = new AccountInfoRepository();
            AccountInfo acc = new AccountInfo();
            acc.AccountNumber = 3000;
            acc.AccountName = "test";
            acc.ResultReportCategory = 1;
            acc.PartsReportCategory = 2;
            acc.WeekCategory = 3;
            acc.IsIncome = true;
            test.Add(acc);
        }
    }
}
