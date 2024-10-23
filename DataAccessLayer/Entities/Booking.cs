namespace DataAccessLayer.Entities
{
    public class Booking
    {
        public int BookingID { get; set; }
        public string ApplicationUserID { get; set; } = "";
        public int PackageID { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public int NumberOfTravelers { get; set; }
        public bool IsCoupleBooking { get; set; }

        public string Status { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Package Package { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
