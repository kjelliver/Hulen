using System.Collections;
using System.Collections.Generic;
using Hulen.BusinessServices.ServiceModel;

namespace Hulen.WebCode.ViewModels
{
    public class ResultIndexWebModel
    {
        public IEnumerable<Result> Results { get; set; }
        public IEnumerable<string> Years { get; set; }
        public string SelectedYear { get; set; }
        public string DefaultYear { get; set; }
    }

    public class ResultDeleteWebModel
    {
        public Result SelectedResult { get; set; }
    }

    public class ResultImportWebModel
    {
        public IEnumerable<string> PeriodList { get; set; }
        public int Year { get; set; }
        public string Comment { get; set; }

        public List<ResultAccount> FailedAccounts { get; set; }
        public string Period { get; set; }

        public IEnumerable BudgetStatusList { get; set; }

        public string UsedBudget { get; set; }

        public IEnumerable<AccountInfo> Accounts { get; set; }
    }
}