namespace DataAccessLayer.DTOs.OutputModels
{
    public class HotelOutputModel
    {
        public int HotelID { get; set; }
        public string HotelName { get; set; }
        public string Description { get; set; }
        public int StarRating { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string HotelCode { get; set; }
        public int LocationID { get; set; }

        // Only the ImageUrl from HotelImages
        public List<string> ImageUrls { get; set; }
    }

}
