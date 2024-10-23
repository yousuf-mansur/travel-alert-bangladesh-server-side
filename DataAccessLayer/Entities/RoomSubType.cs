namespace DataAccessLayer.Entities
{
    public class RoomSubType
    {
        public int RoomSubTypeID { get; set; }
        public string SubTypeName { get; set; }
    
        public ICollection<Room> Rooms { get; set; }
    }
}
