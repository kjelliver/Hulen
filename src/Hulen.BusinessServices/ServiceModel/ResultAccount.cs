using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.BusinessServices.ServiceModel
{
    public class ResultAccount
    {
        public Guid Id { get; set; }
        public int AccountNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double AmountMonth { get; set; }
        public double AmountSoFar { get; set; }
        public int RealAccount { get; set; }
    }
}
