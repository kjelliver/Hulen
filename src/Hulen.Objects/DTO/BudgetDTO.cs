using System;

namespace Hulen.Objects.DTO
{
    public class BudgetDTO
    {
        public virtual Guid Id { get; set; }
        public virtual int AccountNumber { get; set; }
        public virtual int Year { get; set; }
        public virtual int BudgetStatus { get; set; }
        public virtual double YearAmount { get; set; }
        public virtual double JanuaryAmount { get; set; }
        public virtual double FebruaryAmount { get; set; }
        public virtual double MarchAmount { get; set; }
        public virtual double AprilAmount { get; set; }
        public virtual double MayAmount { get; set; }
        public virtual double JuneAmount { get; set; }
        public virtual double JulyAmount { get; set; }
        public virtual double AugustAmount { get; set; }
        public virtual double SeptemberAmount { get; set; }
        public virtual double OctoberAmount { get; set; }
        public virtual double NovemberAmount { get; set; }
        public virtual double DecemberAmount { get; set; }
    }
}
