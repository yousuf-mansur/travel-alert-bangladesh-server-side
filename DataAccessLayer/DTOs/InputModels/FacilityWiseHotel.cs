

namespace DataAccessLayer.DTOs.InputModels
{
    public class FacilityWiseHotel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string Description { get; set; }
        public int StarRating { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string HotelCode { get; set; }

        // Foreign Key
        public int LocationId { get; set; }

        public ICollection<HotelFacilityDto> HotelFacilities { get; set; } = new List<HotelFacilityDto>();
    }
}
