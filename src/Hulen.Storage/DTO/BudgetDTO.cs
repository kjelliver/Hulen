using System;

namespace Hulen.Storage.DTO
{
    public class BudgetDTO
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string BudgetStatus { get; set; }
        public string Comment { get; set; }
    }
}