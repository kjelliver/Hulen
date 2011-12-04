using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.Objects.DTO
{
    public class HotelCollectionDTO
    {
        public virtual int Id { get; set; }
        public virtual int HotelId { get; set; }
        public virtual int BudgetId { get; set; }
    }
}
