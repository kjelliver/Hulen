namespace Hulen.Storage.DTO
{
    public class HotelDTO
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int SingleRoomPrice { get; set; }
        public virtual int DoubleRoomPrice { get; set; }
        public virtual int TripleRoomPrice { get; set; }
        public virtual int GroupRoomPrice { get; set; }
        public virtual int ExtraBedPrice { get; set; }
        public virtual bool IsActive { get; set; } 
    }
}
