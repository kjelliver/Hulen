using System;

namespace Hulen.Storage.DTO
{
    public class FixedArrangementCostsDTO
    {
        public virtual int Id { get; set; }
        public virtual DateTime GeneratedDate { get; set; }
        public virtual double PricePerBeer { get; set; }
        public virtual double PricePerWine { get; set; }
        public virtual int FixedTechRental { get; set; }
        public virtual int SoundmanSalery { get; set; }
        public virtual int SoundmanSaleryPerWarmUp { get; set; }
        public virtual int PromotionExpences { get; set; }
        public virtual int FixedCosts { get; set; }
        public virtual int DocumentId { get; set; }
    }
}
