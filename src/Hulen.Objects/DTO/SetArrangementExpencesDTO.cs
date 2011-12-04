using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.Objects.SystemData
{
    public class SetArrangementExpencesDTO
    {
        public virtual int Id { get; set; }
        public virtual DateTime GeneratedDate { get; set; }
        public virtual double PricePerBeer { get; set; }
        public virtual double PricePerWine { get; set; }
        public virtual int SetcTechRental { get; set; }
        public virtual int SoundmanSalery { get; set; }
        public virtual int SoundmanSaleryPerWarmUp { get; set; }
        public virtual int PromotionExpences { get; set; }
        public virtual int SetExpences { get; set; }
        public virtual int DocumentId { get; set; }
    }
}
