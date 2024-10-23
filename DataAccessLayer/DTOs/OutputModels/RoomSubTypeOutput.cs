namespace DataAccessLayer.DTOs.OutputModels
{
    public class RoomSubTypeOutput
    {
        public int RoomSubTypeID { get; set; }
        public string SubTypeName { get; set; }
        public int RoomTypeID { get; set; }
        public RoomTypeOutput RoomType { get; set; } // Including the nested RoomType output model
    }
}
