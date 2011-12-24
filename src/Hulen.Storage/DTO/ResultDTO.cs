using System;

namespace Hulen.Storage.DTO
{
    public class ResultDTO
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
        public string Comment { get; set; }
        public int UsedBudget { get; set; }
    }
}
