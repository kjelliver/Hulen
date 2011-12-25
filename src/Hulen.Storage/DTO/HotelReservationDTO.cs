namespace Hulen.Storage.DTO
{
    public class HotelReservationDTO
    {
        public virtual int Id { get; set; }
        public virtual string HotelName { get; set; }
        public virtual int BudgetId { get; set; }

        public virtual int SingleRooms { get; set; }
        public virtual int SingleRoomPrice { get; set; }

        public virtual int DoubleRooms { get; set; }
        public virtual int DoubleRoomPrice { get; set; }

        public virtual int TripleRooms { get; set; }
        public virtual int TripleRoomPrice { get; set; }

        public virtual int GroupRooms { get; set; }
        public virtual int GroupRoomPrice { get; set; }

        public virtual int ExtraBeds { get; set; }
        public virtual int ExtraBedPrice { get; set; }

    
    }
}
