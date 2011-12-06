namespace Hulen.Objects.DTO
{
    public class HotelReservationDTO
    {
        public virtual int Id { get; set; }
        public virtual int HotelId { get; set; }
        public virtual int BudgetId { get; set; }
        public virtual int SingleRooms { get; set; }
        public virtual int DoubleRooms { get; set; }
        public virtual int TripleRooms { get; set; }
        public virtual int GroupRooms { get; set; }
        public virtual int ExtraBeds { get; set; }
    
    }
}
