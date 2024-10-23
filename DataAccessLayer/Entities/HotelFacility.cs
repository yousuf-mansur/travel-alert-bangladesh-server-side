namespace DataAccessLayer.Entities
{
    public class HotelFacility
    {
        public int HotelFacilityID { get; set; }

        // Foreign Keys
        public int HotelID { get; set; }
        public Hotel Hotel { get; set; }

        public int FacilityID { get; set; }
        public Facility Facility { get; set; }

        // Timestamps
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

}