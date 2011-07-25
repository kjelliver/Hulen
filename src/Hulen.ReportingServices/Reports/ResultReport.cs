using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Hulen.ReportingServices.Reports
{
    public class ResultReport
    {
        private readonly IAccountInfoRepository _accountInfoRepository = new AccountInfoRepository();
        private readonly IResultAccountRepository _resultRepository = new ResultAccountRepository();
        private readonly IBudgetRepository _budgetRepository = new BudgetRepository();
        private readonly IAccountInfoResultsRepository _categoryRepository = new AccountInfoResultsRepository();
        readonly StringBuilder _sb = new StringBuilder();

        public int Month { get; set; }
        public int Year { get; set; }
        public int UsedBudgetStatus { get; set; }
        public IEnumerable<AccountInfoDTO> Accounts { get; set; }
        public IEnumerable<ResultAccountDTO> Result { get; set; }
        public IEnumerable<BudgetDTO> Budget { get; set; }
        public IEnumerable<AccountInfoResultsDTO> ResultCategories { get; set; }
        public double[] SalesIncome { get; set; }
        public double[] OtherIncome { get; set; }
        public double[] GoodsCosts { get; set; }
        public double[] PersonalCosts { get; set; }
        public double[] OtherCosts { get; set; }
        public double[] Financial { get; set; }

        public ResultReport()
        { 
        }

        public ResultReport(int month, int year, int status)
        {
            Month = month;
            Year = year;
            UsedBudgetStatus = status;
        }

        public string GenerateCssStyle()
        {
            _sb.Clear();
            _sb.AppendLine("<style type=\"text/css\">");
            _sb.AppendLine(".rowHeader { text-align:center; background-color: #BBBBBB; font-weight: bold; }");
            _sb.AppendLine(".headerPeriod { text-align:center; background-color: #BBBBBB; font-weight: bold; font-size: 25px; }");
            _sb.AppendLine(".mediumHeader {background-color: #BBBBBB; font-weight: bold; }");
            _sb.AppendLine(".emptyRow {height:20px;}");
            _sb.AppendLine(".tdData { text-align:center; }");
            _sb.AppendLine(".tdSum { text-align:center; font-weight: bold;}");
            _sb.AppendLine("</style>");
            return _sb.ToString();
        }

        public string GenerateHtmlBody()
        {
            _sb.Clear();
            OpenTable();
            GenerateTableHeader();
            GenerateTableData();
            CloseTable();
            return _sb.ToString();
        }

        private void OpenTable()
        {
            _sb.AppendLine("<table cellspacing=0 width=1024px>");
        }

        private void GenerateTableData()
        {
            Accounts = new List<AccountInfoDTO>();
            //Accounts = _accountInfoRepository.GetAllByYear(Year);
            Result = _resultRepository.GetResultByMonth(Month, Year);
            Budget = _budgetRepository.GetBudgetByYearAndStatus(Year, UsedBudgetStatus);
            ResultCategories = _categoryRepository.GetAll();

            InsertIncome();
            InsertCosts();
            InsertFinancial();
            InsertResult();
        }

        private void InsertResult()
        {
            InsertEmptyRow();
            CreateDataRow("tdSum", "", "Resultat før skatt",
                    (SalesIncome[0] + OtherIncome[0]) - (GoodsCosts[0] + PersonalCosts[0] + OtherCosts[0]) + Financial[0],
                    (SalesIncome[1] + OtherIncome[1]) - (GoodsCosts[1] + PersonalCosts[1] + OtherCosts[1]) + Financial[1],
                    (SalesIncome[2] + OtherIncome[2]) - (GoodsCosts[2] + PersonalCosts[2] + OtherCosts[2]) + Financial[2],
                    (SalesIncome[3] + OtherIncome[3]) - (GoodsCosts[3] + PersonalCosts[3] + OtherCosts[3]) + Financial[3]);
            InsertEmptyRow();
        }

        private void InsertFinancial()
        {
            double resultMonth;
            double resultYear;
            double budgetMonth;
            double budgetYear;

            IEnumerable<AccountInfoDTO> accounts = Accounts.Where(x => x.ResultReportCategory == ResultCategories.Single(y => y.Id == 6).Id);

            InsertEmptyRow();
            InsertMediumHeader("Finansielle inntekter og kostnader");
            foreach (AccountInfoDTO account in accounts)
            {
                resultMonth = GetResultMonth(account.AccountNumber);
                resultYear = GetResultYear(account.AccountNumber);
                budgetMonth = GetBudgetMonth(Month, account.AccountNumber);
                budgetYear = GetBudgetYear(account.AccountNumber);
                CreateDataRow("tdData", account.AccountNumber.ToString(), account.AccountName, budgetMonth, resultMonth, budgetYear, resultYear);
            }
            Financial = CalculateFinancialSum(accounts);
        }

        private double[] CalculateFinancialSum(IEnumerable<AccountInfoDTO> accounts)
        {
            var budgetMonth = new double();
            var budgetYear = new double();
            var resultMonth = new double();
            var resultYear = new double();

            foreach (AccountInfoDTO account in accounts)
            {
                if (account.IsIncome)
                {
                    resultMonth += GetResultMonth(account.AccountNumber);
                    resultYear += GetResultYear(account.AccountNumber);
                    budgetMonth += GetBudgetMonth(Month, account.AccountNumber);
                    budgetYear += GetBudgetYear(account.AccountNumber);
                }
                else
                {
                    resultMonth -= GetResultMonth(account.AccountNumber);
                    resultYear -= GetResultYear(account.AccountNumber);
                    budgetMonth -= GetBudgetMonth(Month, account.AccountNumber);
                    budgetYear -= GetBudgetYear(account.AccountNumber);
                }
            }
            CreateDataRow("tdSum", "", "Netto finansielle poster", budgetMonth, resultMonth, budgetYear, resultYear);
            return new double[] { budgetMonth, resultMonth, budgetYear, resultYear };
        }

        private void InsertIncome()
        {
            IEnumerable<AccountInfoDTO> salesIncomeAccounts = Accounts.Where(x => x.ResultReportCategory == ResultCategories.Single(y => y.Id == 1).Id);
            double budgetMonth;
            double budgetYear;
            double resultMonth;
            double resultYear;

            InsertEmptyRow();
            InsertMediumHeader("Salgsinntekter");
            foreach(AccountInfoDTO account in salesIncomeAccounts)
            {
                resultMonth = GetResultMonth(account.AccountNumber);
                resultYear = GetResultYear(account.AccountNumber);
                budgetMonth = GetBudgetMonth(Month, account.AccountNumber);
                budgetYear = GetBudgetYear(account.AccountNumber);
                CreateDataRow("tdData", account.AccountNumber.ToString(), account.AccountName, budgetMonth, resultMonth, budgetYear, resultYear);
            }
            SalesIncome = CalculateSum(salesIncomeAccounts, "Salgsinntekter");

            salesIncomeAccounts = Accounts.Where(x => x.ResultReportCategory == ResultCategories.Single(y => y.Id == 2).Id);
            InsertEmptyRow();
            InsertMediumHeader("Andre inntekter");
            foreach (AccountInfoDTO account in salesIncomeAccounts)
            {
                resultMonth = GetResultMonth(account.AccountNumber);
                resultYear = GetResultYear(account.AccountNumber);
                budgetMonth = GetBudgetMonth(Month, account.AccountNumber);
                budgetYear = GetBudgetYear(account.AccountNumber);
                CreateDataRow("tdData", account.AccountNumber.ToString(), account.AccountName, budgetMonth, resultMonth, budgetYear, resultYear);
            }
            OtherIncome =  CalculateSum(salesIncomeAccounts, "Andre inntekter");

            InsertEmptyRow();
            CreateDataRow("tdSum", "", "Totale driftsinntekter", SalesIncome[0] + OtherIncome[0], SalesIncome[1] + OtherIncome[1], SalesIncome[2] + OtherIncome[2], SalesIncome[3] + OtherIncome[3]);
            InsertEmptyRow();
        }

        private void InsertCosts()
        {
            double budgetMonth;
            double budgetYear;
            double resultMonth;
            double resultYear;

            var costsAccounts = Accounts.Where(x => x.ResultReportCategory == ResultCategories.Single(y => y.Id == 3).Id);
            InsertEmptyRow();
            InsertMediumHeader("Varekjøp");
            foreach (AccountInfoDTO account in costsAccounts)
            {
                resultMonth = GetResultMonth(account.AccountNumber);
                resultYear = GetResultYear(account.AccountNumber);
                budgetMonth = GetBudgetMonth(Month, account.AccountNumber);
                budgetYear = GetBudgetYear(account.AccountNumber);
                CreateDataRow("tdData", account.AccountNumber.ToString(), account.AccountName, budgetMonth, resultMonth, budgetYear, resultYear);
            }
            GoodsCosts = CalculateSum(costsAccounts, "Varekjøp");

            costsAccounts = Accounts.Where(x => x.ResultReportCategory == ResultCategories.Single(y => y.Id == 4).Id);
            InsertEmptyRow();
            InsertMediumHeader("Personalkostnader");
            foreach (AccountInfoDTO account in costsAccounts)
            {
                resultMonth = GetResultMonth(account.AccountNumber);
                resultYear = GetResultYear(account.AccountNumber);
                budgetMonth = GetBudgetMonth(Month, account.AccountNumber);
                budgetYear = GetBudgetYear(account.AccountNumber);
                CreateDataRow("tdData", account.AccountNumber.ToString(), account.AccountName, budgetMonth, resultMonth, budgetYear, resultYear);
            }
            PersonalCosts = CalculateSum(costsAccounts, "Personalkostnader");

            costsAccounts = Accounts.Where(x => x.ResultReportCategory == ResultCategories.Single(y => y.Id == 5).Id);
            InsertEmptyRow();
            InsertMediumHeader("Andre driftskostnader");
            foreach (AccountInfoDTO account in costsAccounts)
            {
                resultMonth = GetResultMonth(account.AccountNumber);
                resultYear = GetResultYear(account.AccountNumber);
                budgetMonth = GetBudgetMonth(Month, account.AccountNumber);
                budgetYear = GetBudgetYear(account.AccountNumber);
                CreateDataRow("tdData", account.AccountNumber.ToString(), account.AccountName, budgetMonth, resultMonth, budgetYear, resultYear);
            }
            OtherCosts = CalculateSum(costsAccounts, "Andre driftskostnader");

            InsertEmptyRow();
            CreateDataRow("tdSum", "", "Totale driftsutgifter", 
                    GoodsCosts[0] + PersonalCosts[0] + OtherCosts[0],
                    GoodsCosts[1] + PersonalCosts[1] + OtherCosts[1],
                    GoodsCosts[2] + PersonalCosts[2] + OtherCosts[2],
                    GoodsCosts[3] + PersonalCosts[3] + OtherCosts[3]);
            InsertEmptyRow();
        }

        private double[] CalculateSum(IEnumerable<AccountInfoDTO> salesIncomeAccounts, string name)
        {
            var budgetMonth = new double();
            var budgetYear = new double();
            var resultMonth = new double();
            var resultYear = new double();

            foreach (AccountInfoDTO account in salesIncomeAccounts)
            {
                resultMonth += GetResultMonth(account.AccountNumber);
                resultYear += GetResultYear(account.AccountNumber);
                budgetMonth += GetBudgetMonth(Month, account.AccountNumber);
                budgetYear += GetBudgetYear(account.AccountNumber);
            }
            CreateDataRow("tdSum", "", name, budgetMonth, resultMonth, budgetYear, resultYear);
            return new double[] { budgetMonth, resultMonth, budgetYear, resultYear };
        }

        private void InsertMediumHeader(string name)
        {
            _sb.AppendLine("<tr><td class=\"mediumHeader\"></td><td colspan=\"7\" class=\"mediumHeader\">" + name + "</td></tr>");
        }

        private void InsertEmptyRow()
        {
            _sb.AppendLine("<tr><td colspan=\"8\" class=\"emptyRow\"></td></tr>");
        }

        private void CreateDataRow(string cssClass, string accountNumber, string accountName, double budgetMonth, double resultThisMonth, double budgetYear, double resultYear)
        {
            _sb.AppendLine("<tr>");

            _sb.AppendLine("<td>");
            _sb.AppendLine(accountNumber);
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td>");
            _sb.AppendLine(accountName);
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=\"" + cssClass + "\">");
            _sb.AppendLine(String.Format("{0:0.00}",budgetMonth));
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=\"" + cssClass + "\">");
            _sb.AppendLine(String.Format("{0:0.00}", resultThisMonth));
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=\"" + cssClass + "\">");
            _sb.AppendLine("0");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=\"" + cssClass + "\">");
            _sb.AppendLine(String.Format("{0:0.00}", budgetYear));
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=\"" + cssClass + "\">");
            _sb.AppendLine(String.Format("{0:0.00}", resultYear));
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=\"" + cssClass + "\">");
            _sb.AppendLine("0");
            _sb.AppendLine("</td>");

            _sb.AppendLine("</tr>");
        }

        private void GenerateTableHeader()
        {
            _sb.AppendLine("<tr>");

            _sb.AppendLine("<td rowspan=\"2\" class=\"rowHeader\">");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td rowspan=\"2\" class=\"headerPeriod\">");
            _sb.AppendLine(GetMonthYear(Month, Year));
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td colspan = \"3\" class=\"rowHeader\">");
            _sb.AppendLine("Perioden");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td colspan = \"3\" class=\"rowHeader\">");
            _sb.AppendLine("Hittil i år");
            _sb.AppendLine("</td>");

            _sb.AppendLine("</tr>");

            _sb.AppendLine("<tr>");

            _sb.AppendLine("<td class=\"rowHeader\">");
            _sb.AppendLine("Budsjett");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=\"rowHeader\">");
            _sb.AppendLine("Regnskap");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=\"rowHeader\">");
            _sb.AppendLine("Fjorår");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=\"rowHeader\">");
            _sb.AppendLine("Budsjett");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=\"rowHeader\">");
            _sb.AppendLine("Regnskap");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=\"rowHeader\">");
            _sb.AppendLine("Fjorår");
            _sb.AppendLine("</td>");

            _sb.AppendLine("</tr>");
        }

        private string GetMonthYear(int month, int year)
        {
            string returnExp = "";
            if (month == 1)
                returnExp = "Januar, ";
            if (month == 2)
                returnExp = "Februar, ";
            if (month == 3)
                returnExp = "Mars, ";
            if (month == 4)
                returnExp = "April, ";
            if (month == 5)
                returnExp = "Mai, ";
            if (month == 6)
                returnExp = "Juni, ";
            if (month == 7)
                returnExp = "Juli, ";
            if (month == 8)
                returnExp = "August, ";
            if (month == 9)
                returnExp = "September, ";
            if (month == 10)
                returnExp = "Oktober, ";
            if (month == 11)
                returnExp = "November, ";
            if (month == 12)
                returnExp = "Desember, ";
            return returnExp + year;
        }

        private void CloseTable()
        {
            _sb.AppendLine("</table>");
        }

        private double GetResultMonth(int accountNumber)
        {
            var resultMonth = new double();
            resultMonth =  Result.Where(x => x.AccountNumber == accountNumber).Aggregate(resultMonth, (current, temp) => current + temp.AmountMonth);
            return resultMonth;
        }

        private double GetResultYear(int accountNumber)
        {
            var resultYear = new double();
            resultYear = Result.Where(x => x.AccountNumber == accountNumber).Aggregate(resultYear, (current, temp) => current + temp.AmountSoFar);
            return resultYear;
        }

        private double GetBudgetMonth(int month, int accountNumber)
        {
            var budgetMonth = new double();
            if (month == 1)
                budgetMonth = Budget.Where(x => x.AccountNumber == accountNumber).Aggregate(budgetMonth, (current, temp) => current + temp.JanuaryAmount);
            if (month == 2)
                budgetMonth = Budget.Where(x => x.AccountNumber == accountNumber).Aggregate(budgetMonth, (current, temp) => current + temp.FebruaryAmount);
            if (month == 3)
                budgetMonth = Budget.Where(x => x.AccountNumber == accountNumber).Aggregate(budgetMonth, (current, temp) => current + temp.MarchAmount);
            if (month == 4)
                budgetMonth = Budget.Where(x => x.AccountNumber == accountNumber).Aggregate(budgetMonth, (current, temp) => current + temp.AprilAmount);
            if (month == 5)
                budgetMonth = Budget.Where(x => x.AccountNumber == accountNumber).Aggregate(budgetMonth, (current, temp) => current + temp.MayAmount);
            if (month == 6)
                budgetMonth = Budget.Where(x => x.AccountNumber == accountNumber).Aggregate(budgetMonth, (current, temp) => current + temp.JuneAmount);
            if (month == 7)
                budgetMonth = Budget.Where(x => x.AccountNumber == accountNumber).Aggregate(budgetMonth, (current, temp) => current + temp.JulyAmount);
            if (month == 8)
                budgetMonth = Budget.Where(x => x.AccountNumber == accountNumber).Aggregate(budgetMonth, (current, temp) => current + temp.AugustAmount);
            if (month == 9)
                budgetMonth = Budget.Where(x => x.AccountNumber == accountNumber).Aggregate(budgetMonth, (current, temp) => current + temp.SeptemberAmount);
            if (month == 10)
                budgetMonth = Budget.Where(x => x.AccountNumber == accountNumber).Aggregate(budgetMonth, (current, temp) => current + temp.OctoberAmount);
            if (month == 11)
                budgetMonth = Budget.Where(x => x.AccountNumber == accountNumber).Aggregate(budgetMonth, (current, temp) => current + temp.NovemberAmount);
            if (month == 12)
                budgetMonth = Budget.Where(x => x.AccountNumber == accountNumber).Aggregate(budgetMonth, (current, temp) => current + temp.DecemberAmount);
            return budgetMonth;
        }

        private double GetBudgetYear(int accountNumber)
        {
            var budgetYear = new double();
            for (int month = 1; month <= Month; month++)
            {
                budgetYear += GetBudgetMonth(month, accountNumber);
            }
            return budgetYear;
        }
    }
}
