using System;
using System.Collections;
using System.Collections.Generic;
using Hulen.Objects.DTO;

namespace Hulen.WebCode.Models
{
    public class ResultIndexWebModel
    {
        public IEnumerable<ResultDTO> Results { get; set; }
        public IEnumerable<int> Years { get; set; }
        public int SelectedYear { get; set; }
        public int DefaultYear { get; set; }
    }

    public class ResultDeleteWebModel
    {
        public ResultDTO SelectedResult { get; set; }
    }

    public class ResultImportWebModel
    {
        public IEnumerable<string> PeriodList { get; set; }
        public int Year { get; set; }
        public string Comment { get; set; }

        public List<ResultAccountDTO> FailedAccounts { get; set; }
        public string Period { get; set; }

        public IEnumerable BudgetStatusList { get; set; }

        public string UsedBudget { get; set; }
    }
}