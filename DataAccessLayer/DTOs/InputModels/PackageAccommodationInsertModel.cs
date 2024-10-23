namespace DataAccessLayer.DTOs.InputModels
{
    public class PackageAccommodationInsertModel
    {
        public int PackageID { get; set; } = 1; // Default value
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomID { get; set; } = 1; // Default value
        public decimal price { get; set; } = 2000; // Default value
    }

}
