using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.Objects.ViewModels
{
    public class AccountInfoViewModel
    {
        public Guid Id { get; set; }
        public int AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string ResultReportCategory { get; set; }
        public string PartsReportCategory { get; set; }
        public string WeekCategory { get; set; }
        public string IsIncome { get; set; }
        public int Year { get; set; }
        public string NumberAndName { get; set; }
    }
}
