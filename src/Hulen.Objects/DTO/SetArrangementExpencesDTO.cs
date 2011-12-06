using System;

namespace Hulen.Objects.DTO
{
    public class SetArrangementExpencesDTO
    {
        public virtual int Id { get; set; }
        public virtual DateTime GeneratedDate { get; set; }
        public virtual double PricePerBeer { get; set; }
        public virtual double PricePerWine { get; set; }
        public virtual int SetTechRental { get; set; }
        public virtual int SoundmanSalery { get; set; }
        public virtual int SoundmanSaleryPerWarmUp { get; set; }
        public virtual int PromotionExpences { get; set; }
        public virtual int SetExpences { get; set; }
        public virtual int DocumentId { get; set; }
    }
}
