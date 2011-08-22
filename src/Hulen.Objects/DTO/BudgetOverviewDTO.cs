using System;

namespace Hulen.Objects.DTO
{
    public class BudgetOverviewDTO
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string BudgetStatus { get; set; }
        public string Comment { get; set; }
    }
}