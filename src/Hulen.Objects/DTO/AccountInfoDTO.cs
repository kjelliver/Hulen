using System;

namespace Hulen.Objects.DTO
{
    public class AccountInfoDTO
    {
        public virtual Guid Id { get; set; }
        public virtual int AccountNumber { get; set; }
        public virtual string AccountName { get; set; }
        public virtual int ResultReportCategory { get; set; }
        public virtual int PartsReportCategory { get; set; }
        public virtual int WeekCategory { get; set; }
        public virtual bool IsIncome { get; set; }
        public virtual int Year { get; set; }
    }
}
