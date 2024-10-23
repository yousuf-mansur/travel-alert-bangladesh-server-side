using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Package
    {
        public int PackageID { get; set; }
        public string PackageTitle { get; set; } = "";
        public string PackageDescription { get; set; } = "";
        public bool IsAvailable { get; set; }

        public int PackageCategoryID { get; set; }
        public PackageCategory PackageCategory { get; set; }

        public int? PackageSubCategoryID { get; set; }
        public virtual PackageSubCategory? PackageSubCategory { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public ICollection<PackageGallery> PackageGallery { get; set; } = new List<PackageGallery>();
        public virtual ICollection<PackageDetails> PackageDetails { get; set; } = new List<PackageDetails>();
        public ICollection<PackageFacility> PackageFacilities { get; set; } = new List<PackageFacility>();
        public ICollection<Schedule> Schedule { get; set; } = new List<Schedule>();
        public ICollection<PackageFAQ> PackageFAQ { get; set; } = new List<PackageFAQ>();
        public ICollection<PackageUser> PackageUsers { get; set; } = new List<PackageUser>();
        public ICollection<DayWiseTourCost> DayWiseTourCosts { get; set; } = new List<DayWiseTourCost>();
        public ICollection<PackageIncludes> PackageIncludes { get; set; } = new List<PackageIncludes>();
        public ICollection<PackageExcludes> PackageExcludes { get; set; } = new List<PackageExcludes>();
        public ICollection<PackageAccommodation> PackageAccommodations { get; set; } = new List<PackageAccommodation>();
        public ICollection<PackageTransportation> PackageTransportations { get; set; } = new List<PackageTransportation>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<PackageLocation> PackageLocations { get; set; } = new List<PackageLocation>();
        public virtual ICollection<TourVoucher> TourVouchers { get; set; } = new List<TourVoucher>();




    }


}
