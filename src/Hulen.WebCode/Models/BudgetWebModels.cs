using System.Collections.Generic;
using Hulen.Objects.ViewModels;

namespace Hulen.WebCode.Models
{
    public class BudgetIndexWebModel
    {
        public IEnumerable<BudgetOverviewViewModel> StoredBudgets { get; set; }
    }
}