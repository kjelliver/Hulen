using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;

namespace Hulen.WebCode.Models
{
    public class FileImportWebModel
    {
    }

    public class BudgetImportWebModelOld
    {
        public string BudgetYear { get; set; }
        public List<string> BudgetStatusList { get; set; }
        public string BudgetStatus { get; set; }
    } 

    public class ResultAccountImportWebModel
    {
        public string Month { get; set; }
        public string ResultYear { get; set; }

        
        public string FailedAccountsList { get; set; }
        public List<ResultAccountDTO> FailedAccountsCollection { get; set; }
        public ResultAccountDTO ResultAccount { get; set; }
    }
}
