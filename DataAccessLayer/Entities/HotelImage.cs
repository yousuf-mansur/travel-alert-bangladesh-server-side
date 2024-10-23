namespace DataAccessLayer.Entities
{
    public class HotelImage
    {
        public int HotelImageID { get; set; }
        public string ImageUrl { get; set; } // Non-nullable as images are required
        //public string ImageResolution { get; set; }
        public string Caption { get; set; }
        public bool IsThumbnail { get; set; } // Corrected typo
        public DateTime CreatedOn { get; set; }

        // Foreign Key
        public int HotelID { get; set; }
        public Hotel Hotel { get; set; }
    }

}