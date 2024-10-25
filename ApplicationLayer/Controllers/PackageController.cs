using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;
using DataAccessLayer.DTOs.InputModels;
using DataAccessLayer.DTOs.OutputModels;
using DataAccessLayer.Data;


namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PackageController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        #region Package

        [HttpPost("add/package")]
        public async Task<IActionResult> CreatePackage([FromBody] PackageInsertModel model, [FromQuery] string? customUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_context.PackageCategories.Any(c => c.PackageCategoryID == model.PackageCategoryID))
            {
                return BadRequest(new { success = false, message = "Invalid PackageCategoryID." });
            }


            // Check if the provided PackageCategoryID exists
            var categoryExists = await _context.PackageCategories
                .AnyAsync(c => c.PackageCategoryID == model.PackageCategoryID);
            if (!categoryExists)
            {
                return BadRequest("Invalid PackageCategoryID.");
            }

            // Check if the provided PackageSubCategoryID exists if it is not null
            if (model.PackageSubCategoryID.HasValue)
            {
                var subCategoryExists = await _context.PackageSubCategories
                    .AnyAsync(sc => sc.PackageSubCategoryID == model.PackageSubCategoryID);
                if (!subCategoryExists)
                {
                    return BadRequest("Invalid PackageSubCategoryID.");
                }
            }

            var package = new Package
            {
                PackageTitle = model.PackageTitle,
                PackageDescription = model.PackageDescription,
                IsAvailable = model.IsAvailable,
                PackageCategoryID = model.PackageCategoryID,
                PackageSubCategoryID = model.PackageSubCategoryID
            };

            _context.Packages.Add(package);
            await _context.SaveChangesAsync();

            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                  .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                  .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService?.RequestUrl?.Url + "/" + package.PackageID;
            }



            return CreatedAtAction(nameof(CreatePackage), new { packageId = package.PackageID }, new
            {
                success = true,
                message = "Package created successfully.",
                packageId = package.PackageID,
                url = requestUrl
            });
        }

        [HttpGet("get/all/packages")]
        public async Task<IActionResult> GetAllPackages()
        {
            var packages = await _context.Packages
                .Select(p => new
                {
                    p.PackageID,
                    p.PackageTitle,
                    p.PackageDescription,
                    p.IsAvailable,
                    p.PackageCategoryID,
                    p.PackageSubCategoryID,
                    p.CreatedAt,
                    p.UpdatedAt
                })
                .ToListAsync();

            if (packages == null || !packages.Any())
            {
                return NotFound(new { success = false, message = "No packages found." });
            }

            return Ok(new { success = true, data = packages });
        }


        [HttpGet("get/package/{packageId}")]
        public async Task<IActionResult> GetPackageById(int packageId)
        {
            var package = await _context.Packages
                .Where(p => p.PackageID == packageId)
                .Select(p => new
                {
                    p.PackageID,
                    p.PackageTitle,
                    p.PackageDescription,
                    p.IsAvailable,
                    p.PackageCategoryID,
                    p.PackageSubCategoryID,
                    p.CreatedAt,
                    p.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (package == null)
            {
                return NotFound(new { success = false, message = "Package not found." });
            }

            return Ok(new { success = true, data = package });
        }

        [HttpPut("update-package/{packageId}")]
        public async Task<IActionResult> UpdatePackage(int packageId, [FromBody] PackageInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var package = await _context.Packages.FindAsync(packageId);
            if (package == null)
            {
                return NotFound(new { success = false, message = "Package not found." });
            }

            // Update the package fields
            package.PackageTitle = model.PackageTitle;
            package.PackageDescription = model.PackageDescription;
            package.IsAvailable = model.IsAvailable;
            package.PackageCategoryID = model.PackageCategoryID;
            package.PackageSubCategoryID = model.PackageSubCategoryID;
            package.UpdatedAt = DateTime.Now; // Update the UpdatedAt field

            _context.Packages.Update(package);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Package updated successfully.",
                packageId = package.PackageID,
                updated = package.UpdatedAt
            });
        }


        #endregion

        #region PackageDetails

        [HttpPost("packagedetails/add")]
        public async Task<IActionResult> AddPackageDetails(int packageId, [FromBody] PackageDetailsInsertModel model)
        {
            var package = await _context.Packages.FindAsync(packageId);
            if (package == null)
            {
                return NotFound(new { success = false, message = "Package not found." });
            }


            var packageDetails = new PackageDetails
            {
                PackageID = packageId,
                PackageDuration = model.PackageDuration,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                PickupPoint = model.PickupPoint,
                MaximumPerson = model.MaximumPerson,
                MinimumPerson = model.MinimumPerson
            };

            
            _context.PackageDetails.Add(packageDetails);
            await _context.SaveChangesAsync();



            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                 .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                 .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService?.RequestUrl?.Url + "/" + packageId;
            }


            return Ok(new
            {
                success = true,
                message = "Package details added successfully.",
                packageDetailsId = packageDetails.PackageDetailsID,
                packageId = packageId ,
                url = requestUrl
            });
        }


        [HttpGet("packagedetails/get/{packageId}")]
        public async Task<IActionResult> GetPackageDetailsByPackageId(int packageId)
        {
            var packageDetails = await _context.PackageDetails
                .Where(pd => pd.PackageID == packageId)
                .Select(pd => new
                {
                    pd.PackageDetailsID,
                    pd.PackageID,
                    pd.PackageDuration,
                    pd.StartDate,
                    pd.EndDate,
                    pd.PickupPoint,
                    pd.MaximumPerson,
                    pd.MinimumPerson,
                    pd.CreatedAt,
                    pd.UpdatedAt
                })
                .ToListAsync();

            if (packageDetails == null || !packageDetails.Any())
            {
                return NotFound(new { success = false, message = "No package details found for this package." });
            }

            return Ok(new { success = true, data = packageDetails });
        }


       [HttpPut("packagedetails/update/{packageId}")]
        public async Task<IActionResult> UpdatePackageDetails(int packageId, [FromBody] PackageDetailsInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageDetails = await _context.PackageDetails.FindAsync(packageId);
            if (packageDetails == null)
            {
                return NotFound(new { success = false, message = "Package details not found." });
            }

            packageDetails.PackageDuration = model.PackageDuration;
            packageDetails.StartDate = model.StartDate;
            packageDetails.EndDate = model.EndDate;
            packageDetails.PickupPoint = model.PickupPoint;
            packageDetails.MaximumPerson = model.MaximumPerson;
            packageDetails.MinimumPerson = model.MinimumPerson;
            packageDetails.UpdatedAt = DateTime.Now; 

            _context.PackageDetails.Update(packageDetails);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Package details updated successfully.",
                packageId = packageDetails.PackageID
            });
        }

        #endregion

        #region Location
        
        [HttpPost("add-package-location/{packageId}")]
        public async Task<ActionResult> PostPackageLocation(int packageId, PackageLocationInsertModel model)
        {
            // Validate that the packageId in the route matches the model's PackageID
            if (packageId != model.PackageID)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Package ID in the URL does not match the Package ID in the request body."
                });
            }

            var packageLocation = new PackageLocation
            {
                PackageID = model.PackageID,
                LocationID = model.LocationID
            };

            _context.PackageLocation.Add(packageLocation);
            await _context.SaveChangesAsync();

            // Return success message in the specified format
            return Ok(new
            {
                success = true,
                message = "Package location added successfully.",
                packageLocationID = packageLocation.PackageLocationID,
                packageID = packageLocation.PackageID,
                locationID = packageLocation.LocationID
            });
        }


        // GET: api/PackageLocation/package/{packageId}
        [HttpGet("package-location/{packageId}")]
        public async Task<ActionResult<PackageLocation>> GetLocationByPackageId(int packageId)
        {
            // Retrieve the package location with the specified packageId including related data
            var packageLocation = await _context.PackageLocation
                .Include(pl => pl.Location) // Include the Location entity
                    .ThenInclude(l => l.State) // Include the State from Location
                        .ThenInclude(s => s.Country) // Include the Country from State
                .Include(pl => pl.Location.LocationGalleries) // Include LocationGalleries from Location
                .FirstOrDefaultAsync(pl => pl.PackageID == packageId);

            if (packageLocation == null)
            {
                return NotFound();
            }

            // Return the package location with related entities
            return Ok(new
            {
                packageLocationID = packageLocation.PackageLocationID,
                packageID = packageLocation.PackageID,
                location = new
                {
                    locationID = packageLocation.Location.LocationID,
                    locationName = packageLocation.Location.LocationName,
                    locationDescription = packageLocation.Location.LocationDescription,
                    state = new
                    {
                        stateID = packageLocation.Location.State.StateID,
                        stateName = packageLocation.Location.State.StateName,
                        country = new
                        {
                            countryID = packageLocation.Location.State.Country.CountryID,
                            countryName = packageLocation.Location.State.Country.CountryName
                        }
                    },
                    locationGalleries = packageLocation.Location.LocationGalleries.Select(g => new
                    {
                        locationGalleryID = g.LocationGalleryID,
                        imageUrl = g.ImageUrl,
                        isPrimary = g.IsPrimary,
                        imageCaption = g.ImageCaption
                    }).ToList()
                }
            });
        }

        #endregion

        #region Package Gallery

        [HttpPost("add/package/gallery")]
        public async Task<IActionResult> AddPackageGallery([FromForm] PackageGalleryInsertModel model)
        {
            if (model.ImageFile == null || model.ImageFile.Length == 0)
            {
                return BadRequest(new { success = false, message = "Image file is required." });
            }

            var imageUrl = await SaveImage(model.ImageFile);

            var packageGallery = new PackageGallery
            {
                ImageUrl = imageUrl,
                IsPrimary = model.IsPrimary,
                ImageCaption = model.ImageCaption,
                PackageID = model.PackageID
            };

            _context.PackageGallery.Add(packageGallery);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Package gallery image added successfully.",
                packageGalleryId = packageGallery.PackageGalleryID,
                packageId = model.PackageID
            });
        }


        [HttpGet("get/package/gallery/{packageId}")]
        public async Task<IActionResult> GetAllImagesByPackageId(int packageId)
        {
            var package = await _context.Packages
                .Where(p => p.PackageID == packageId)
                .Select(p => new { p.PackageID, p.PackageTitle })
                .FirstOrDefaultAsync();

            if (package == null)
            {
                return NotFound(new { success = false, message = "Package not found." });
            }

            var images = await _context.PackageGallery
                .Where(g => g.PackageID == packageId)
                .Select(g => new
                {
                    g.PackageGalleryID,
                    g.ImageUrl,
                    g.IsPrimary,
                    g.ImageCaption
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                packageId = package.PackageID,
                packageTitle = package.PackageTitle,
                images = images  // Return direct array instead of nested object
            });
        }




        //[HttpGet("get/package/gallery/{packageId}")]
        //public async Task<IActionResult> GetAllImagesByPackageId(int packageId)
        //{
        //    var package = await _context.Packages
        //        .Where(p => p.PackageID == packageId)
        //        .Select(p => new { p.PackageID, p.PackageTitle })
        //        .FirstOrDefaultAsync();

        //    if (package == null)
        //    {
        //        return NotFound(new { success = false, message = "Package not found." });
        //    }

        //    var images = await _context.PackageGallery
        //        .Where(g => g.PackageID == packageId)
        //        .Select(g => new
        //        {
        //            g.PackageGalleryID,
        //            g.ImageUrl,
        //            g.IsPrimary,
        //            g.ImageCaption
        //        })
        //        .ToListAsync();

        //    if (images.Count == 0)
        //    {
        //        return NotFound(new { success = false, message = "No images found for this package." });
        //    }

        //    return Ok(new
        //    {
        //        success = true,
        //        packageId = package.PackageID,
        //        packageTitle = package.PackageTitle,
        //        images
        //    });
        //}

        [HttpPut("update/package/gallery/{galleryId}")]
        public async Task<IActionResult> UpdatePackageGallery(int galleryId, [FromForm] PackageGalleryInsertModel model)
        {
            var existingGallery = await _context.PackageGallery.FirstOrDefaultAsync(g => g.PackageGalleryID == galleryId);

            if (existingGallery == null)
            {
                return NotFound(new { success = false, message = "Gallery image not found." });
            }

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(existingGallery.ImageUrl))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingGallery.ImageUrl);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                var newImageUrl = await SaveImage(model.ImageFile);
                existingGallery.ImageUrl = newImageUrl;
            }

            existingGallery.IsPrimary = model.IsPrimary;
            existingGallery.ImageCaption = model.ImageCaption;

            _context.PackageGallery.Update(existingGallery);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Package gallery image updated successfully.",
                packageGalleryId = existingGallery.PackageGalleryID,
                packageId = existingGallery.PackageID
            });
        }


        #endregion

        #region Package Facility

        [HttpPost("add/package/facility")]
        public async Task<IActionResult> AddPackageFacility([FromBody] PackageFacilityInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageFacilities = new List<PackageFacility>();

            foreach (var facilityId in model.FacilityIDs)
            {
                var packageFacility = new PackageFacility
                {
                    PackageID = model.PackageID,
                    FacilityID = facilityId
                };

                packageFacilities.Add(packageFacility);
            }

            await _context.PackageFacilities.AddRangeAsync(packageFacilities);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddPackageFacility), new { packageID = model.PackageID }, new
            {
                success = true,
                message = "Package facilities added successfully.",
                packageID = model.PackageID,
                facilityIDs = model.FacilityIDs
            });
        }

        #endregion

        #region FAQ  

        [HttpPost("add-package-faq")]
        public async Task<IActionResult> AddPackageFAQ([FromBody] PackageFAQInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageFAQ = new PackageFAQ
            {
                FAQTitle = model.FAQTitle,
                FAQDescription = model.FAQDescription,
                PackageID = model.PackageID
            };

            _context.PackageFAQ.Add(packageFAQ);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddPackageFAQ), new { packageFAQID = packageFAQ.PackageFAQID }, new
            {
                success = true,
                message = "Package FAQ added successfully.",
                packageFAQID = packageFAQ.PackageFAQID
            });
        }

        
        
        [HttpGet("get-package-faqs/{packageId}")]
        public async Task<IActionResult> GetAllFAQsByPackageId(int packageId)
        {

            var package = await _context.Packages
                .Where(p => p.PackageID == packageId)
                .Select(p => new
                {
                    p.PackageID,
                    p.PackageTitle
                })
                .FirstOrDefaultAsync();

            if (package == null)
            {
                return NotFound(new { success = false, message = "Package not found." });
            }
            var faqs = await _context.PackageFAQ
                .Where(f => f.PackageID == packageId)
                .Select(f => new
                {
                    f.PackageFAQID,
                    f.FAQTitle,
                    f.FAQDescription
                })
                .ToListAsync();

            if (faqs.Count == 0)
            {
                return NotFound(new { success = false, message = "No FAQs found for this package." });
            }

            return Ok(new
            {
                success = true,
                packageId = package.PackageID,
                packageTitle = package.PackageTitle,
                faqs
            });
        }




        [HttpPut("update-package-faq/{faqId}")]
        public async Task<IActionResult> UpdatePackageFAQ(int faqId, [FromBody] PackageFAQInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageFAQ = await _context.PackageFAQ.FindAsync(faqId);
            if (packageFAQ == null)
            {
                return NotFound(new { success = false, message = "FAQ not found." });
            }

            packageFAQ.FAQTitle = model.FAQTitle;
            packageFAQ.FAQDescription = model.FAQDescription;
            packageFAQ.PackageID = model.PackageID;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Package FAQ updated successfully.",
                faqId = packageFAQ.PackageFAQID
            });
        }

        [HttpDelete("delete-package-faq/{faqId}")]
        public async Task<IActionResult> DeletePackageFAQ(int faqId)
        {
            var packageFAQ = await _context.PackageFAQ.FindAsync(faqId);
            if (packageFAQ == null)
            {
                return NotFound(new { success = false, message = "FAQ not found." });
            }

            _context.PackageFAQ.Remove(packageFAQ);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Package FAQ deleted successfully.",
                faqId = packageFAQ.PackageFAQID
            });
        }

        #endregion

        #region Package Includes


        [HttpPost("packageincludes/add")]
        public async Task<IActionResult> AddPackageInclude([FromBody] PackageIncludesInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageInclude = new PackageIncludes
            {
                PackageID = model.PackageID,
                IncludeDescription = model.IncludeDescription
            };

            _context.PackageIncludes.Add(packageInclude);
            await _context.SaveChangesAsync();

            // URL manipulation
            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath); // Assume UrlTask.RemoveLastSegment is a utility method

            // Retrieve the URL service for the path
            var urlService = await _context.UrlServices
                .Include(u => u.RequestUrl)
                .Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            // Determine the redirect URL
            var requestUrl = urlService == null ? "dashboard" : $"{urlService.RequestUrl.Url}/{packageInclude.IncludeID}";

            return CreatedAtAction(nameof(AddPackageInclude), new { includeId = packageInclude.IncludeID }, new
            {
                success = true,
                message = "Package include added successfully.",
                includeId = packageInclude.IncludeID,
                packageId = model.PackageID,
                Url = requestUrl
            });
        }

        [HttpGet("packageincludes/get/{packageId}")]
        public async Task<IActionResult> GetPackageIncludesByPackageId(int packageId)
        {
            var packageExists = await _context.Packages.AnyAsync(p => p.PackageID == packageId);
            if (!packageExists)
            {
                return NotFound(new { success = false, message = "Package not found." });
            }

            var packageIncludes = await _context.PackageIncludes
                .Where(pi => pi.PackageID == packageId)
                .Select(pi => new
                {
                    pi.IncludeID,
                    pi.IncludeDescription
                })
                .ToListAsync();

            if (packageIncludes.Count == 0)
            {
                return NotFound(new { success = false, message = "No includes found for this package." });
            }

            // URL manipulation
            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath); 

            // Retrieve the URL service for the path
            var urlService = await _context.UrlServices
                .Include(u => u.RequestUrl)
                .Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            // Determine the redirect URL
            var requestUrl = urlService == null ? "dashboard" : $"{urlService.RequestUrl.Url}/{packageId}";

            return Ok(new
            {
                success = true,
                packageId,
                includes = packageIncludes,
                Url = requestUrl
            });
        }

        [HttpPut("packageincludes/update/{packageId}")]
        public async Task<IActionResult> UpdatePackageInclude(int packageId, [FromBody] PackageIncludesUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingInclude = await _context.PackageIncludes.FirstOrDefaultAsync(pi => pi.PackageID == packageId);
            if (existingInclude == null)
            {
                return NotFound(new { success = false, message = "Package include not found." });
            }

            existingInclude.PackageID = model.PackageID;
            existingInclude.IncludeDescription = model.IncludeDescription;

            _context.PackageIncludes.Update(existingInclude);
            await _context.SaveChangesAsync();

            // URL manipulation
            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath); 

            // Retrieve the URL service for the path
            var urlService = await _context.UrlServices
                .Include(u => u.RequestUrl)
                .Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            // Determine the redirect URL
            var requestUrl = urlService == null ? "dashboard" : $"{urlService.RequestUrl.Url}/{packageId}";

            return Ok(new
            {
                success = true,
                message = "Package include updated successfully.",
                includeId = existingInclude.IncludeID,
                packageId = existingInclude.PackageID,
                Url = requestUrl
            });
        }


        //[HttpDelete("packageinclude/delete/{includeId}")]
        //public async Task<IActionResult> DeletePackageInclude(int includeId)
        //{
        //    var existingInclude = await _context.PackageIncludes.FirstOrDefaultAsync(pi => pi.IncludeID == includeId);

        //    if (existingInclude == null)
        //    {
        //        return NotFound(new { success = false, message = "Package include not found." });
        //    }

        //    _context.PackageIncludes.Remove(existingInclude);
        //    await _context.SaveChangesAsync();

        //    // URL manipulation
        //    var request = HttpContext.Request;
        //    var rowPath = request.Path;
        //    var path = UrlTask.RemoveLastSegment(rowPath); 

        //    // Retrieve the URL service for the path
        //    var urlService = await _context.UrlServices
        //        .Include(u => u.RequestUrl)
        //        .Include(u => u.CurrentUrl)
        //        .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

        //    // Determine the redirect URL
        //    var requestUrl = urlService == null ? "dashboard" : $"{urlService.RequestUrl.Url}/{includeId}";

        //    return Ok(new
        //    {
        //        success = true,
        //        message = "Package include deleted successfully.",
        //        includeId = existingInclude.IncludeID,
        //        packageId = existingInclude.PackageID,
        //        Url = requestUrl
        //    });
        //}
        #endregion

        #region Package Excludes

        [HttpPost("packageexcludes/add")]
        public async Task<IActionResult> AddPackageExclude([FromBody] PackageExcludesInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageExclude = new PackageExcludes
            {
                PackageID = model.PackageID,
                ExcludeDescription = model.ExcludeDescription
            };

            _context.PackageExcludes.Add(packageExclude);
            await _context.SaveChangesAsync();

            // URL manipulation
            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                .Include(u => u.RequestUrl)
                .Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = urlService == null ? "dashboard" : $"{urlService.RequestUrl.Url}/{packageExclude.ExcludeID}";

            return CreatedAtAction(nameof(AddPackageExclude), new { excludeId = packageExclude.ExcludeID }, new
            {
                success = true,
                message = "Package exclude added successfully.",
                excludeId = packageExclude.ExcludeID,
                packageId = packageExclude.PackageID,
                Url = requestUrl
            });
        }



        [HttpGet("packageexcludes/get/{packageId}")]
        public async Task<IActionResult> GetPackageExcludesByPackageId(int packageId)
        {
            var packageExists = await _context.Packages.AnyAsync(p => p.PackageID == packageId);
            if (!packageExists)
            {
                return NotFound(new { success = false, message = "Package not found." });
            }

            var packageExcludes = await _context.PackageExcludes
                .Where(pe => pe.PackageID == packageId)
                .Select(pe => new
                {
                    pe.ExcludeID,
                    pe.ExcludeDescription
                })
                .ToListAsync();

            if (packageExcludes.Count == 0)
            {
                return NotFound(new { success = false, message = "No excludes found for this package." });
            }

            // URL manipulation
            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                .Include(u => u.RequestUrl)
                .Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = urlService == null ? "dashboard" : $"{urlService.RequestUrl.Url}/{packageId}";

            return Ok(new
            {
                success = true,
                packageId,
                excludes = packageExcludes,
                Url = requestUrl
            });
        }

        [HttpPut("packageexcludes/update/{packageId}")]
        public async Task<IActionResult> UpdatePackageExclude(int packageId, [FromBody] PackageExcludesUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingExclude = await _context.PackageExcludes.FirstOrDefaultAsync(pe => pe.PackageID == packageId);
            if (existingExclude == null)
            {
                return NotFound(new { success = false, message = "Package exclude not found." });
            }

            existingExclude.PackageID = model.PackageID;
            existingExclude.ExcludeDescription = model.ExcludeDescription;

            _context.PackageExcludes.Update(existingExclude);
            await _context.SaveChangesAsync();

            // URL manipulation
            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                .Include(u => u.RequestUrl)
                .Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = urlService == null ? "dashboard" : $"{urlService.RequestUrl.Url}/{packageId}";

            return Ok(new
            {
                success = true,
                message = "Package exclude updated successfully.",
                excludeId = existingExclude.ExcludeID,
                packageId = existingExclude.PackageID,
                Url = requestUrl
            });
        }

        //[HttpDelete("packageexclude/delete/{excludeId}")]
        //public async Task<IActionResult> DeletePackageExclude(int excludeId)
        //{
        //    var existingExclude = await _context.PackageExcludes.FirstOrDefaultAsync(pe => pe.ExcludeID == excludeId);

        //    if (existingExclude == null)
        //    {
        //        return NotFound(new { success = false, message = "Package exclude not found." });
        //    }

        //    _context.PackageExcludes.Remove(existingExclude);
        //    await _context.SaveChangesAsync();

        //    // URL manipulation
        //    var request = HttpContext.Request;
        //    var rowPath = request.Path;
        //    var path = UrlTask.RemoveLastSegment(rowPath);

        //    var urlService = await _context.UrlServices
        //        .Include(u => u.RequestUrl)
        //        .Include(u => u.CurrentUrl)
        //        .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

        //    var requestUrl = urlService == null ? "dashboard" : $"{urlService.RequestUrl.Url}/{excludeId}";

        //    return Ok(new
        //    {
        //        success = true,
        //        message = "Package exclude deleted successfully.",
        //        excludeId = existingExclude.ExcludeID,
        //        packageId = existingExclude.PackageID,
        //        Url = requestUrl
        //    });
        //}

        #endregion

        #region Accommodation

        [HttpPost("accommodations/add")]
        public async Task<IActionResult> AddPackageAccommodation([FromBody] PackageAccommodationInsertModel model)
        {
            var packageAccommodation = new PackageAccommodation
            {
                PackageID = model.PackageID,
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                RoomID = model.RoomID,
                price = model.price
            };

            _context.PackageAccommodations.Add(packageAccommodation);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Package accommodation added successfully.",
                packageAccommodationID = packageAccommodation.PackageAccommodationID
            });
        }



        [HttpGet("accommodations/get/{packageId}")]
        public async Task<IActionResult> GetPackageAccommodations(int packageId)
        {
            //var accommodations = await _context.PackageAccommodations
            //    .Where(a => a.PackageID == packageId)
            //    .ToListAsync();

            var accommodations = await _context.PackageAccommodations
    .Where(a => a.PackageID == packageId)
    .Select(a => new {
        a.PackageAccommodationID,
        a.PackageID,
        a.CheckInDate,
        a.CheckOutDate,
        a.RoomID,
        a.price
    })
    .ToListAsync();

            // No need to check for 'null' as ToListAsync() always returns a list
            if (!accommodations.Any())
            {
                return NotFound(new { success = false, message = "No accommodations found for this package." });
            }

            return Ok(new
            {
                success = true,
                accommodations
            });
        }

        #endregion

        #region Save Images

        private async Task<string> SaveImage(IFormFile imageFile)
        {
            var fileExtension = Path.GetExtension(imageFile.FileName);

            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(imageFile.FileName)}_{Guid.NewGuid()}{fileExtension}";


            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Uploads", "Packages");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return Path.Combine("Uploads", "Packages", uniqueFileName);
        }         


        #endregion

        #region Voucher

        [HttpPost("voucher/add")]
        public async Task<IActionResult> AddTourVoucher([FromForm] TourVoucherInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? voucherUrl = null;

            if (model.VoucherFile != null)
            {
                voucherUrl = await SaveVoucher(model.VoucherFile);
            }

            var tourVoucher = new TourVoucher
            {
                TourVoucherCode = model.TourVoucherCode,
                VoucherUrl = voucherUrl
            };

            _context.TourVouchers.Add(tourVoucher);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Tour voucher added successfully.",
                tourVoucherID = tourVoucher.TourVoucherID,
                voucherUrl = voucherUrl
            });
        }

        [HttpGet("voucher/get")]
        public async Task<IActionResult> GetTourVouchers()
        {
            var tourVouchers = await _context.TourVouchers
                .Select(tv => new
                {
                    tv.TourVoucherID,
                    tv.TourVoucherCode,
                    tv.VoucherUrl
                })
                .ToListAsync();

            if (!tourVouchers.Any())
            {
                return NotFound(new { success = false, message = "No tour vouchers found." });
            }

            return Ok(tourVouchers);
        }

        [HttpGet("voucher/get/{id}")]
        public async Task<IActionResult> GetTourVoucherById(int id)
        {
            var tourVoucher = await _context.TourVouchers
                .Where(tv => tv.TourVoucherID == id)
                .Select(tv => new
                {
                    tv.TourVoucherID,
                    tv.TourVoucherCode,
                    tv.VoucherUrl
                })
                .FirstOrDefaultAsync();

            if (tourVoucher == null)
            {
                return NotFound(new { success = false, message = "Tour voucher not found." });
            }

            return Ok(tourVoucher);
        }

        [HttpPut("voucher/edit/{id}")]
        public async Task<IActionResult> UpdateTourVoucher(int id, [FromForm] TourVoucherInsertModel model)
        {
            var tourVoucher = await _context.TourVouchers.FindAsync(id);
            if (tourVoucher == null)
            {
                return NotFound(new { success = false, message = "Tour voucher not found." });
            }

            tourVoucher.TourVoucherCode = model.TourVoucherCode;

            if (model.VoucherFile != null)
            {
                if (!string.IsNullOrEmpty(tourVoucher.VoucherUrl))
                {
                    var oldFilePath = Path.Combine(_hostEnvironment.WebRootPath, tourVoucher.VoucherUrl);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                tourVoucher.VoucherUrl = await SaveVoucher(model.VoucherFile);
            }

            _context.TourVouchers.Update(tourVoucher);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Tour voucher updated successfully.", tourVoucherID = tourVoucher.TourVoucherID });
        }



        [HttpDelete("voucher/delete/{id}")]
        public async Task<IActionResult> DeleteTourVoucher(int id)
        {
            var tourVoucher = await _context.TourVouchers.FindAsync(id);
            if (tourVoucher == null)
            {
                return NotFound(new { success = false, message = "Tour voucher not found." });
            }

            if (!string.IsNullOrEmpty(tourVoucher.VoucherUrl))
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Vouchers");
                var filePath = Path.Combine(uploadsFolder, Path.GetFileName(tourVoucher.VoucherUrl));

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.TourVouchers.Remove(tourVoucher);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Tour voucher deleted successfully." });
        }

        private async Task<string> SaveVoucher(IFormFile imageFile)
        {
            var fileExtension = Path.GetExtension(imageFile.FileName);

            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(imageFile.FileName)}_{Guid.NewGuid()}{fileExtension}";


            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Uploads", "Voucher");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return Path.Combine($"Uploads/Voucher/{uniqueFileName}");
        }

        #endregion

        #region Schedule

        [HttpPost("schedule/add/{packageID}")]
        public async Task<IActionResult> AddSchedule(int packageID, ScheduleInsertModel model)
        {

            var schedule = new Schedule
            {
                // ScheduleID should not be set
                TourVoucherID = model.TourVoucherID,
                ScheduleTitle = model.ScheduleTitle,
                ScheduleDescription = model.ScheduleDescription,
                PackageID = packageID,
                DayNumber = model.DayNumber,
                TentativeTime = model.TentativeTime,
                ActualTime = model.ActualTime,
                TentativeCost = model.TentativeCost,
                ActualCost = model.ActualCost,
                DayCostCategoryID = model.DayCostCategoryID
                //Category = model.Category

            };



            await _context.Schedule.AddAsync(schedule); 
            await _context.SaveChangesAsync();





            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                 .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                 .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService?.RequestUrl?.Url + "/" + packageID;
            }





            return Ok(new
            {
                success = true,
                message = "Schedule added successfully.",
                scheduleID = schedule.ScheduleID, 
                packageID = schedule.PackageID,
                requestUrl
            });
        }

        [HttpGet("schedule/get/{packageID}")]
        public async Task<IActionResult> GetSchedulesByPackageID(int packageID)
           {
            var schedules = await _context.Schedule
                .Where(s => s.PackageID == packageID)
                .Select(s => new
                {
                    s.ScheduleID,
                    s.TourVoucherID,
                    s.ScheduleTitle,
                    s.ScheduleDescription,
                    s.PackageID,
                    s.DayNumber,
                    s.TentativeTime,
                    s.ActualTime,
                    s.TentativeCost,
                    s.ActualCost,
                    s.DayCostCategoryID, 
                    s.CreatedAt,
                    s.UpdatedAt,
                    PackageTitle = s.Package != null ? s.Package.PackageTitle : null
                })
                .ToListAsync();

            if (schedules == null || !schedules.Any())
            {
                return NotFound(new { success = false, message = "No schedules found for the package." });
            }

            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                  .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                  .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService?.RequestUrl?.Url + "/" + packageID;
            }

            return Ok(new { success = true, packageTitle = schedules.FirstOrDefault()?.PackageTitle, data = schedules , url = requestUrl});
        }


        [HttpGet("schedule/{packageID}/{scheduleID}")]
        public async Task<IActionResult> GetScheduleById(int packageID, int scheduleID)
        {
            
            // Get the specific schedule details
            var schedule = await _context.Schedule
                .Where(s => s.PackageID == packageID && s.ScheduleID == scheduleID)
                .Select(s => new
                {
                    s.ScheduleID,
                    s.TourVoucherID,
                    s.ScheduleTitle,
                    s.ScheduleDescription,
                    s.PackageID,
                    s.DayNumber,
                    s.TentativeTime,
                    s.ActualTime,
                    s.TentativeCost,
                    s.ActualCost,
                    s.DayCostCategoryID, 
                    
                    s.CreatedAt,
                    s.UpdatedAt,
                    PackageTitle = s.Package != null ? s.Package.PackageTitle : null 
                })
                .FirstOrDefaultAsync();

            if (schedule == null)
            {
                return NotFound(new { success = false, message = "Schedule not found." });
            }


            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService?.RequestUrl?.Url + "/" + packageID + "/" + scheduleID;
            }

            return Ok(new
            {
                success = true,
                data = schedule,
                url = requestUrl
                
            });
        }



        [HttpPut("schedule/edit/{scheduleID}")]
        public async Task<IActionResult> UpdateSchedule(int scheduleID, [FromBody] ScheduleInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schedule = await _context.Schedule.FindAsync(scheduleID);
            if (schedule == null)
            {
                return NotFound(new { message = "Schedule not found." });
            }

            schedule.TourVoucherID = model.TourVoucherID;
            schedule.ScheduleTitle = model.ScheduleTitle;
            schedule.ScheduleDescription = model.ScheduleDescription;
           
            schedule.DayNumber = model.DayNumber;
            schedule.TentativeTime = model.TentativeTime;
            schedule.ActualTime = model.ActualTime;
            schedule.TentativeCost = model.TentativeCost;
            schedule.ActualCost = model.ActualCost;

            // Set the DayCostCategoryID from the model
            schedule.DayCostCategoryID = model.DayCostCategoryID;

            schedule.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                  .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                  .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService?.RequestUrl?.Url +  "/" + scheduleID;
            }


            return Ok(new
            {
                success = true,
                message = "Schedule updated successfully.",
                url = requestUrl,
            });
        }

        //[HttpGet("schedulesCost/{packageID}")]
        //public async Task<IActionResult> GetSchedulesCostPackageID(ScheduleCostInputModel scheduleCostInputModel)
        //{
        //    if (scheduleCostInputModel.DayCostCatagory == 1)
        //    {
        //        var cost = await _context.PackageFoodItems.Where(e=> e.PackageID == scheduleCostInputModel.PackageId && e.PackageDayNumber == scheduleCostInputModel.DayNumber).FirstOrDefaultAsync();

        //        return Ok(new 
        //        {
        //            tentiveTime = cost.ScheduleTime,
        //            tentiveCost = cost.ItemTotalCost, 
        //        });

        //    }
        //    if (scheduleCostInputModel.DayCostCatagory == 2)
        //    {
        //        var cost = await _context.PackageAccommodations.Where(e => e.PackageID == scheduleCostInputModel.PackageId ).FirstOrDefaultAsync();

        //        return Ok(new
        //        {
        //            tentiveTime = cost.CheckInDate,
                   
        //        });

        //    }
        //    if (scheduleCostInputModel.DayCostCatagory == 3)
        //    {
        //        var cost = await _context.PackageTransportations.Where(e => e.PackageID == scheduleCostInputModel.PackageId ).FirstOrDefaultAsync();

        //        return Ok(new
        //        {
        //            tentiveTime = cost.ScheduleTime,
        //            tentiveCost = cost.ItemTotalCost,
        //        });

        //    }
        //}

        [HttpDelete("schedule/delete/{scheduleID}")]
        public async Task<IActionResult> DeleteSchedule(int scheduleID)
        {
            var schedule = await _context.Schedule.FindAsync(scheduleID);
            if (schedule == null)
            {
                return NotFound(new { message = "Schedule not found." });
            }

            schedule.DeletedAt = DateTime.Now;
            _context.Schedule.Update(schedule);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Schedule deleted successfully."
            });
        }



        #endregion

        #region PackFood

        [HttpPost("packagefood/add/{packageID}")]
        public async Task<IActionResult> AddPackageFoodItem(int packageID, PackageFoodItemInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemTotalCost = model.FoodQuantity * (decimal)model.FoodUnitPrice;

            var packageFoodItem = new PackageFoodItem
            {
                MealTypeID = model.MealTypeID,
                FoodItemID = model.FoodItemID,
                PackageID = packageID,
                PackageDayNumber = model.PackageDayNumber,
                FoodQuantity = model.FoodQuantity,
                FoodUnitPrice = model.FoodUnitPrice,
                ItemTotalCost = itemTotalCost,
                ScheduleTime = model.ScheduleTime
            };

            _context.PackageFoodItems.Add(packageFoodItem);
            await _context.SaveChangesAsync();



            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                 .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                 .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService?.RequestUrl?.Url + "/" + packageID;
            }


            return CreatedAtAction(nameof(AddPackageFoodItem), new
            {
                packageFoodItemId = packageFoodItem.PackageFoodItemID

            }, new
            {
                success = true,
                message = "Package food item added successfully.",
                packageFoodItemId = packageFoodItem.PackageFoodItemID,
                packageId = packageID,
                Url = requestUrl
            });
        }

        [HttpGet("packagefooditem/get/{packageId}")]
        public async Task<IActionResult> GetPackageFoodItems(int packageId)
        {
            var foodItems = await _context.PackageFoodItems
                .Where(f => f.PackageID == packageId)
                .ToListAsync();

            if (foodItems == null || !foodItems.Any())
            {
                return NotFound(new { success = false, message = "No food items found for this package." });
            }

            return Ok(new
            {
                success = true,
                foodItems
            });
        }

        #endregion

        #region PackTarnsPort

        [HttpPost("transport/add/{packageId}")]
        public async Task<IActionResult> AddPackageTransportation(int packageId, PackageTransportationInsertModel model)
        {
            var itemTransportCost = model.SeatBooked * model.PerHeadTransportCost;

            var packageTransportation = new PackageTransportation
            {
                PackageID = packageId,
                TransportationTypeID = model.TransportationTypeID,
                TransportationCatagoryID = model.TransportationCatagoryID,
                TransportationID = model.TransportationID,
                SeatBooked = model.SeatBooked,
                PackageTransportationDescription = model.PackageTransportationDescription ?? string.Empty, // Ensure it's not null
                PerHeadTransportCost = model.PerHeadTransportCost,
                ItemTransportTotalCost = itemTransportCost
            };

            _context.PackageTransportations.Add(packageTransportation);
            await _context.SaveChangesAsync();




            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);



            var urlService = await _context.UrlServices
                 .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                 .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService?.RequestUrl?.Url + "/" + packageId;
            }

            return Ok(new
            {
                success = true,
                message = "Package transportation added successfully.",
                packageTransportationID = packageTransportation.PackageTransportationID,
                url = requestUrl
            });
        }

        

        [HttpGet("transport/get/{packageId}")]
        public async Task<IActionResult> GetPackageTransportation(int packageId)
        {
            var transportationItems = await _context.PackageTransportations
                .Where(t => t.PackageID == packageId)
                .ToListAsync();

            if (transportationItems == null || !transportationItems.Any())
            {
                return NotFound(new { success = false, message = "No transportation items found for this package." });
            }




            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);



            var urlService = await _context.UrlServices
                 .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                 .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService?.RequestUrl?.Url + "/" + packageId;
            }

            return Ok(new
            {
                success = true,
                transportationItems,
                url = requestUrl
            });
        }

        #endregion

        #region Budget

        [HttpPost("add-package-budget")]
        public async Task<IActionResult> AddPackageBudget([FromBody] PackageBudgetInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Calculate the estimated costs
            var estimatedFoodCost = await _context.PackageFoodItems
                .Where(pfi => pfi.PackageID == model.PackageID)
                .SumAsync(pfi => pfi.ItemTotalCost);

            var estimatedTransportCost = await _context.PackageTransportations
                .Where(pt => pt.PackageID == model.PackageID)
                .SumAsync(pt => pt.ItemTransportTotalCost);

            var estimatedAccommodationCost = await _context.PackageAccommodations
                .Where(pa => pa.PackageID == model.PackageID)
                .SumAsync(pa => pa.price);

            var packageBudget = new PackageBudget
            {
                PackageID = model.PackageID,
                EstimateedFoodCost = estimatedFoodCost,
                EstimatedTransportCost = estimatedTransportCost,
                EstimatedAccomodationCost = estimatedAccommodationCost,
                OtherCost = model.OtherCost,
                ProfitPercent = model.ProfitPercent
            };

            _context.PackageBudgets.Add(packageBudget);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Package budget added successfully.",
                packageBudgetID = packageBudget.PackageBudgetID
            });
        }

        [HttpPut("update-package-budget-othercost/{packageId}")]
        public async Task<IActionResult> UpdatePackageBudgetOtherCost(int packageId, PackageBudgetInsertModelPart model)
        {
            var packageBudget = await _context.PackageBudgets
                .FirstOrDefaultAsync(pb => pb.PackageID == packageId);

            if (packageBudget == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Package budget not found for the specified package."
                });
            }

            packageBudget.OtherCost = model.OtherCost;
            packageBudget.ProfitPercent = model.ProfitPercent;
           

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Other cost updated successfully.",
                packageBudgetID = packageBudget.PackageBudgetID,
                updatedOtherCost = packageBudget.OtherCost
            });
        }


        [HttpGet("get-package-budget/{packageId}")]
        public async Task<IActionResult> GetPackageBudget(int packageId)
        {
            var estimatedFoodCost = await _context.PackageFoodItems
                .Where(pfi => pfi.PackageID == packageId)
                .SumAsync(pfi => pfi.ItemTotalCost);

            
            var estimatedTransportCost = await _context.PackageTransportations
                .Where(pt => pt.PackageID == packageId)
                .SumAsync(pt => pt.ItemTransportTotalCost);

            
            var estimatedAccommodationCost = await _context.PackageAccommodations
                .Where(pa => pa.PackageID == packageId)
                .SumAsync(pa => pa.price);


            var packageBudget = await _context.PackageBudgets
                .FirstOrDefaultAsync(pb => pb.PackageID == packageId);

            if (packageBudget == null)
            {
                return NotFound(new { message = "Package budget not found for the given package." });
            }

            
            var maximumPerson = await _context.PackageDetails
                .Where(pd => pd.PackageID == packageId)
                .Select(pd => pd.MaximumPerson)
                .FirstOrDefaultAsync();

            if (maximumPerson == 0)
            {
                return BadRequest(new { message = "Invalid or missing maximum person count for the package." });
            }

           
            var subtotal = estimatedFoodCost + estimatedTransportCost + estimatedAccommodationCost + packageBudget.OtherCost;

            var profit = (subtotal * packageBudget.ProfitPercent) / 100;


            var totalPackageCost = subtotal + profit;

            
            var individualPackageCost = totalPackageCost / maximumPerson;

            return Ok(new
            {
                PackageID = packageBudget.PackageID,
                EstimateedFoodCost = estimatedFoodCost,
                EstimatedTransportCost = estimatedTransportCost,
                EstimatedAccomodationCost = estimatedAccommodationCost,
                OtherCost = packageBudget.OtherCost,
                ProfitPercent = packageBudget.ProfitPercent,
                Subtotal = subtotal,
                Profit = profit,
                TotalPackageCost = totalPackageCost,
                IndividualPackageCost = individualPackageCost
            });
        }




        [HttpGet]
        public async Task<IActionResult> CalculateAndInsertCosts(int packageId)
        {
            var schedules = await _context.Schedule
                .Where(s => s.PackageID == packageId)
                .ToListAsync();

            var groupedCosts = schedules
                .GroupBy(s => s.DayCostCategoryID)
                .Select(g => new DayWiseTourCost
                {
                    PackageID = packageId,
                    DayCostCategoryID = g.Key,
                    OtherCost = g.Sum(s => s.ActualCost ?? 0), 
                    TotalCost = g.Sum(s => s.ActualCost ?? 0) + 0 
                });

            _context.DayWiseTourCosts.AddRange(groupedCosts);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Costs calculated and stored successfully." });
        }

        [HttpGet("get-daily-total-cost/{packageID}/{dayNumber}")]
        public async Task<IActionResult> GetDailyTotalCost(int packageID, int dayNumber)
        {

            var totalCost = await _context.DayWiseTourCosts
                .Where(d => d.PackageID == packageID && d.DayCostCategoryID == dayNumber)
                .SumAsync(d => d.TotalCost);

            if (totalCost == 0)
            {
                return NotFound(new { success = false, message = "No costs found for the specified package and day number." });
            }

            return Ok(new { success = true, packageID, dayNumber, totalCost });
        }

        [HttpGet("get-total-cost-by-category/{packageID}/{dayCostCategoryID}")]
        public async Task<IActionResult> GetTotalCostByCategory(int packageID, int dayCostCategoryID)
        {

            var totalCost = await _context.DayWiseTourCosts
                .Where(d => d.PackageID == packageID && d.DayCostCategoryID == dayCostCategoryID)
                .SumAsync(d => d.TotalCost);

            if (totalCost == 0)
            {
                return NotFound(new { success = false, message = "No costs found for the specified package and day cost category." });
            }

            return Ok(new { success = true, packageID, dayCostCategoryID, totalCost });
        }

        #endregion

        public static string RemoveLastSegment(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }

           
            url = url.TrimStart('/');
            if (url.StartsWith("api/"))
            {
                url = url.Substring(4); 
            }

            var segments = url.Split('/');

            if (segments.Length > 1)
            {
                var lastSegment = segments[^1];

                if (int.TryParse(lastSegment, out _))
                {
                    return string.Join("/", segments, 0, segments.Length - 1);
                }
            }
            return url;
        }
    }
}
