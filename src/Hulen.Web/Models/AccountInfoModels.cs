using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hulen.Web.Models
{
    public class AccountInfoModel
    {
        public Guid Id { get; set; }
        public int AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string ResultReportCategory { get; set; }
        public string PartsReportCategory { get; set; }
        public string WeekCategory { get; set; }
        public string IsIncome { get; set; }

        public List<String> PartsCategories { get; set; }
        public List<String> ResultCategories { get; set; }
        public List<String> WeekCategories { get; set; }
        public List<String> IsIncomes { get; set; }
    }
}