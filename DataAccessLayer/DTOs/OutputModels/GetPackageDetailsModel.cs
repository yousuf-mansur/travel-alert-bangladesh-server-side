using DataAccessLayer.Entities;
namespace DataAccessLayer.DTOs.OutputModels
{
    public class GetPackageDetailsModel
    {
        public int PackageID { get; set; }
        public string PackageTitle { get; set; } = "";
        public string PackageDescription { get; set; } = "";
        public bool IsAvailable { get; set; }
        public int PackageCategoryID { get; set; }
        public string PackageCategoryName { get; set; } = "";
        public int? PackageSubCategoryID { get; set; }
        public string? PackageSubCategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<PackageGallery> PackageGallery { get; set; } = new List<PackageGallery>();
        public List<PackageDetails> PackageDetails { get; set; } = new List<PackageDetails>();
        public List<PackageFacility> PackageFacilities { get; set; } = new List<PackageFacility>();
        public List<Schedule> Schedule { get; set; } = new List<Schedule>();
        public List<PackageFAQ> PackageFAQ { get; set; } = new List<PackageFAQ>();
        public List<PackageUser> PackageUsers { get; set; } = new List<PackageUser>();
        public List<DayWiseTourCost> DayWiseTourCosts { get; set; } = new List<DayWiseTourCost>();
        public List<PackageIncludes> PackageIncludes { get; set; } = new List<PackageIncludes>();
        public List<PackageExcludes> PackageExcludes { get; set; } = new List<PackageExcludes>();
        public List<PackageAccommodation> PackageAccommodations { get; set; } = new List<PackageAccommodation>();
        public List<PackageTransportation> PackageTransportations { get; set; } = new List<PackageTransportation>();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public List<PackageLocation> PackageLocations { get; set; } = new List<PackageLocation>();
        public List<TourVoucher> TourVouchers { get; set; } = new List<TourVoucher>();
    }

}
