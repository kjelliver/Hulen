using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.Objects.DTO
{
    public class ArrangementBudgetDTO
    {
        public virtual int Id { get; set; }
        public virtual string Artist { get; set; }
        public virtual string SuggestedDate { get; set; }
        public virtual DateTime SetDate { get; set; }
        public virtual int ArtistFee { get; set; }
        public virtual int BeerAmount { get; set; }
        public virtual int BeerExpences { get; set; }
        public virtual int WineAmount { get; set; }
        public virtual int WineExpences { get; set; }
        public virtual int NumberOfPeopleInBand { get; set; }
        public virtual int BuyOutPerPerson { get; set; }
        public virtual int Catering { get; set; }
        public virtual int SetTechRental { get; set; }        
        public virtual int ExtraTechRental { get; set; }
        public virtual int SoundmanSalery { get; set; }
        public virtual int SoundmanSaleryWarmUp { get; set; }
        public virtual int NumberOfWarmupbands { get; set; }
        public virtual int TicketFee { get; set; }
        public virtual int TonoFee { get; set; }
        public virtual int SetPromotionExpences { get; set; }
        public virtual int ExtraPromotionExpences { get; set; }
        public virtual int SetExpences { get; set; }
        public virtual int TicketPrice { get; set; }
        public virtual int ExpectedNumberOfVisitors { get; set; }
        public virtual int PromotorsFee { get; set; }
        public virtual double PromotorsFeePercent { get; set; }
        public virtual int BreakEvenToArtist { get; set; }
        public virtual int DocumentId { get; set; }
    }
}
