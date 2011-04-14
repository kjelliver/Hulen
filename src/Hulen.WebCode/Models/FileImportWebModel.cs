using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.WebCode.Models
{
    public class FileImportWebModel
    {
    }

    public class AccountInfoImportWebModel
    {
        public string AccountInfoYear { get; set; }
    }

    public class BudgetImportWebModel
    {
        public string BudgetYear { get; set; }
        public List<string> BudgetStatusList { get; set; }
        public string BudgetStatus { get; set; }
    } 

    public class ResultAccountImportWebModel
    {
        public string Month { get; set; }
        public string ResultYear { get; set; }
    }
}
