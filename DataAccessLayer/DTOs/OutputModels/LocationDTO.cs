namespace DataAccessLayer.DTOs.OutputModels
{
    public class LocationDTO
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public List<string> LocationGalleries { get; set; }
        public List<string> HotelNames { get; set; }
    }
}
