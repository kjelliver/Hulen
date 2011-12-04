using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;

namespace Hulen.Objects.Models
{
    public class ArrangementBudget
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string SuggestedDate { get; set; }
        public DateTime SetDate { get; set; }
        public int ArtistFee { get; set; }
        public int BeerAmount { get; set; }
        public int BeerExpences { get; set; }
        public int WineAmount { get; set; }
        public int WineExpences { get; set; }
        public HotelCollectionDTO Hotels { get; set; }
        public int NumberOfPeopleInBand { get; set; }
        public int BuyOutPerPerson { get; set; }
        public int Catering { get; set; }
        public int SetTechRental { get; set; }
        public int ExtraTechRental { get; set; }
        public int SoundmanSalery { get; set; }
        public int SoundmanSaleryWarmUp { get; set; }
        public int NumberOfWarmupbands { get; set; }
        public int TicketFee { get; set; }
        public int TonoFee { get; set; }
        public int SetPromotionExpences { get; set; }
        public int ExtraPromotionExpences { get; set; }
        public int SetExpences { get; set; }
        public int TicketPrice { get; set; }
        public int ExpectedNumberOfVisitors { get; set; }
        public int PromotorsFee { get; set; }
        public double PromotorsFeePercent { get; set; }
        public int BreakEvenToArtist { get; set; }
        public int DocumentId { get; set; }
    }
}
