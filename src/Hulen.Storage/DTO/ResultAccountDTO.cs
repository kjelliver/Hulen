using System;

namespace Hulen.Storage.DTO
{
    public class ResultAccountDTO
    {
        public virtual Guid Id { get; set; }
        public virtual int AccountNumber { get; set; }
        public virtual int Month { get; set; }
        public virtual int Year { get; set; }
        public virtual double AmountMonth { get; set; }
        public virtual double AmountSoFar { get; set; }
        public virtual int RealAccount { get; set; }
    }
}
