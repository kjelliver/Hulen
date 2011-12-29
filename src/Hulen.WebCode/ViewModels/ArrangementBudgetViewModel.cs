using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.ServiceModel;

namespace Hulen.WebCode.ViewModels
{
    public class ArrangementBudgetViewModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public ArrangementBudget ArrangementBudget { get; set; }
        public IEnumerable<ArrangementBudget> ArrangementBudgets { get; set; }

        public IEnumerable<string> Bookers { get; set; }
    }
}
