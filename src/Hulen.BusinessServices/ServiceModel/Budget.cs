using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.BusinessServices.ServiceModel
{
    public class Budget
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string BudgetStatus { get; set; }
        public string Comment { get; set; }
    }
}
