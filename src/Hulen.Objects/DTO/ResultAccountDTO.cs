using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.Objects.DTO
{
    public class ResultAccountDTO
    {
        public virtual Guid Id { get; set; }
        public virtual int AccountNumber { get; set; }
        public virtual int Month { get; set; }
        public virtual int Year { get; set; }
        public virtual double AmountMonth { get; set; }
        public virtual double AmountSoFar { get; set; }
        public virtual int? RealAccount { get; set; }
    }
}
