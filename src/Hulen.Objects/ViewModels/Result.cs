using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.Objects.ViewModels
{
    public class Result
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Period { get; set; }
        public string Comment { get; set; }
        public string UsedBudget { get; set; }
    }
}
