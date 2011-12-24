using System.Collections.Generic;
using Hulen.BusinessServices.ServiceModel;

namespace Hulen.WebCode.ViewModels
{
    public class BudgetIndexWebModel
    {
        public IEnumerable<Budget> StoredBudgets { get; set; }   
    }

    public class BudgetDeleteWebModel
    {
        public Budget SelectedBudget { get; set; }
    }

    public class BudgetImportWebModel
    {
        public string BudgetYear { get; set; }
        public List<string> BudgetStatusList { get; set; }
        public string BudgetStatus { get; set; }
        public string Comment { get; set; }
    } 
}