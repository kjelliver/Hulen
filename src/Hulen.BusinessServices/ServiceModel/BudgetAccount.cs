using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.BusinessServices.ServiceModel
{
    public class BudgetAccount
    {
        public Guid Id { get; set; }
        public int AccountNumber { get; set; }
        public int Year { get; set; }
        public int BudgetStatus { get; set; }
        public double YearAmount { get; set; }
        public double JanuaryAmount { get; set; }
        public double FebruaryAmount { get; set; }
        public double MarchAmount { get; set; }
        public double AprilAmount { get; set; }
        public double MayAmount { get; set; }
        public double JuneAmount { get; set; }
        public double JulyAmount { get; set; }
        public double AugustAmount { get; set; }
        public double SeptemberAmount { get; set; }
        public double OctoberAmount { get; set; }
        public double NovemberAmount { get; set; }
        public double DecemberAmount { get; set; }
    }
}
