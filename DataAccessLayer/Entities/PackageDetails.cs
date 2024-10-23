namespace DataAccessLayer.Entities
{
    public class PackageDetails
    {
        public int PackageDetailsID { get; set; }
        public int PackageID { get; set; }
        public Package Package { get; set; }

        public int PackageDuration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PickupPoint { get; set; }
        public int MaximumPerson { get; set; }
        public int MinimumPerson { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

}
