using System;

namespace Hulen.BusinessServices.ServiceModel
{
    public class FixedArrangementCosts
    {
        public int Id { get; set; }
        public DateTime GeneratedDate { get; set; }
        public double PricePerBeer { get; set; }
        public double PricePerWine { get; set; }
        public int FixedTechRental { get; set; }
        public int SoundmanSalery { get; set; }
        public int SoundmanSaleryPerWarmUp { get; set; }
        public int PromotionExpences { get; set; }
        public int FixedCosts { get; set; }
        public int DocumentId { get; set; }

        public void UpdateGeneratedDate()
        {
            GeneratedDate = DateTime.Now;
        }
    }
}
