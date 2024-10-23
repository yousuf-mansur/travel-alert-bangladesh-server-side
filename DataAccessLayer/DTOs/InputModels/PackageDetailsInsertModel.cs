namespace DataAccessLayer.DTOs.InputModels
{
    public class PackageDetailsInsertModel
    {
        public int PackageDuration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PickupPoint { get; set; } = "";
        public int MaximumPerson { get; set; }
        public int MinimumPerson { get; set; }
    }

}
