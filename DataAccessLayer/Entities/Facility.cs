namespace DataAccessLayer.Entities
{
    public class Facility
    {
        public int FacilityID { get; set; }
        public string FacilityName { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }

        // Navigation property
        public ICollection<PackageFacility> PackageFacilities { get; set; }
        public ICollection<HotelFacility> HotelFacilities { get; set; }
    }

}
