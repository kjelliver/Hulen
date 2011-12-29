using System;
using System.Collections.Generic;

namespace Hulen.BusinessServices.ServiceModel
{
    public class ArrangementBudget
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public DateTime Date { get; set; }
        public int ArtistFee { get; set; }
        //public int BeerAmount { get; set; }
        //public int BeerExpences { get; set; }
        //public int WineAmount { get; set; }
        //public int WineExpences { get; set; }
        //public IList<HotelReservation> HotelReservations { get; set; }
        //public int NumberOfPeopleInBand { get; set; }
        //public int BuyOutPerPerson { get; set; }
        //public int Catering { get; set; }
        //public int SetTechRental { get; set; }
        //public int ExtraTechRental { get; set; }
        //public int SoundmanSalery { get; set; }
        //public int SoundmanSaleryWarmUp { get; set; }
        //public int NumberOfWarmupBands { get; set; }
        //public int TicketFee { get; set; }
        //public int TonoFee { get; set; }
        //public int SetPromotionExpences { get; set; }
        //public int ExtraPromotionExpences { get; set; }
        //public int SetExpences { get; set; }
        //public int TicketPrice { get; set; }
        //public int ExpectedNumberOfVisitors { get; set; }
        //public int PromotorsFee { get; set; }
        //public double PromotorsFeePercent { get; set; }
        //public int BreakEvenToArtist { get; set; }
        public int Status { get; set; }
        public string BookerInCharge { get; set; }
    }
}
