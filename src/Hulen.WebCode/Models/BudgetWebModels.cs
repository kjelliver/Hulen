using System.Collections.Generic;
using Hulen.Objects.DTO;

namespace Hulen.WebCode.Models
{
    public class BudgetIndexWebModel
    {
        public IEnumerable<BudgetDTO> StoredBudgets { get; set; }   
    }

    public class BudgetDeleteWebModel
    {
        public BudgetDTO SelectedBudget { get; set; }
    }

    public class BudgetImportWebModel
    {
        public string BudgetYear { get; set; }
        public List<string> BudgetStatusList { get; set; }
        public string BudgetStatus { get; set; }
        public string Comment { get; set; }
    } 
}