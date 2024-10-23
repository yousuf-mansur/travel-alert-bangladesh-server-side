using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;
using DataAccessLayer.DTOs.InputModels;
using DataAccessLayer.DTOs.OutputModels;
using DataAccessLayer.Data;



namespace ApplicationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackageGetController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PackageGetController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/package/{packageId}
        [HttpGet("{packageId}")]
        public async Task<ActionResult<GetPackageDetailsModel>> GetPackageDetails(int packageId)
        {
            var package = await _context.Packages
                .Include(p => p.PackageCategory)
                .Include(p => p.PackageSubCategory)
                .Include(p => p.PackageGallery)
                .Include(p => p.PackageDetails)
                .Include(p => p.PackageFacilities)
                .Include(p => p.Schedule)
                .Include(p => p.PackageFAQ)
                .Include(p => p.PackageUsers)
                .Include(p => p.DayWiseTourCosts)
                .Include(p => p.PackageIncludes)
                .Include(p => p.PackageExcludes)
                .Include(p => p.PackageAccommodations)
                .Include(p => p.PackageTransportations)
                .Include(p => p.Reviews)
                .Include(p => p.Bookings)
                .Include(p => p.PackageLocations)
                .Include(p => p.TourVouchers)
                .FirstOrDefaultAsync(p => p.PackageID == packageId);

            if (package == null)
            {
                return NotFound();
            }

            var packageDetails = new GetPackageDetailsModel
            {
                PackageID = package.PackageID,
                PackageTitle = package.PackageTitle,
                PackageDescription = package.PackageDescription,
                IsAvailable = package.IsAvailable,
                PackageCategoryID = package.PackageCategoryID,
                PackageCategoryName = package.PackageCategory?.CategoryName, // Assuming CategoryName is a property in PackageCategory
                PackageSubCategoryID = package.PackageSubCategoryID,
                PackageSubCategoryName = package.PackageSubCategory?.SubCategoryName, // Assuming SubCategoryName is a property in PackageSubCategory
                CreatedAt = package.CreatedAt,
                UpdatedAt = package.UpdatedAt,
                PackageGallery = package.PackageGallery.ToList(),
                PackageDetails = package.PackageDetails.ToList(),
                PackageFacilities = package.PackageFacilities.ToList(),
                Schedule = package.Schedule.ToList(),
                PackageFAQ = package.PackageFAQ.ToList(),
                PackageUsers = package.PackageUsers.ToList(),
                DayWiseTourCosts = package.DayWiseTourCosts.ToList(),
                PackageIncludes = package.PackageIncludes.ToList(),
                PackageExcludes = package.PackageExcludes.ToList(),
                PackageAccommodations = package.PackageAccommodations.ToList(),
                PackageTransportations = package.PackageTransportations.ToList(),
                Reviews = package.Reviews.ToList(),
                Bookings = package.Bookings.ToList(),
                PackageLocations = package.PackageLocations.ToList(),
                TourVouchers = package.TourVouchers.ToList()
            };

            return Ok(packageDetails);
        }
    }

}
