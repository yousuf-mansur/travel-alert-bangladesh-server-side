using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Hotel
    {
        public int HotelID { get; set; }
        public string HotelName { get; set; }
        public string Description { get; set; }
        public int StarRating { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string HotelCode { get; set; }

        // Foreign Key
        public int LocationID { get; set; }
        public Location Location { get; set; }

        // Relationships
        public ICollection<HotelImage> HotelImages { get; set; }

        public ICollection<Room> Rooms { get; set; }

        public ICollection<HotelFacility> HotelFacilities { get; set; }
    }

}
