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
    public class LocationGalleryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public LocationGalleryController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/LocationGallery/location/{locationId}
        [HttpGet("locationGallery/{locationId}")]
        public async Task<ActionResult<IEnumerable<LocationGallery>>> GetGalleriesByLocationId(int locationId)
        {
            var galleries = await _context.LocationGalleries
                .Where(g => g.LocationID == locationId)
                .ToListAsync();

            if (galleries == null || galleries.Count == 0)
            {
                return NotFound();
            }

            return galleries;
        }

        // POST: api/LocationGallery
        [HttpPost("add")]
        public async Task<ActionResult> PostLocationGallery(LocationGalleryInsertModel model)
        {
            if (model.ImageFile == null || model.ImageFile.Length == 0)
            {
                return BadRequest("Image file is required.");
            }

            var locationGallery = new LocationGallery
            {
                IsPrimary = model.IsPrimary,
                ImageCaption = model.ImageCaption,
                LocationID = model.LocationID
            };

            locationGallery.ImageUrl = await SaveGallery(model.ImageFile);
            _context.LocationGalleries.Add(locationGallery);
            await _context.SaveChangesAsync();

            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                  .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                  .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = urlService == null ? "dashboard" : urlService?.RequestUrl?.Url;

            var url = Url.Action(nameof(GetGalleriesByLocationId), new { locationId = locationGallery.LocationID });

            // Return both locationGallery and requestUrl
            return Created(url, new { locationGallery, requestUrl });
        }


        // PUT: api/LocationGallery/{id}
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutLocationGallery(int id, LocationGalleryInsertModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var locationGallery = await _context.LocationGalleries.FindAsync(id);
            if (locationGallery == null)
            {
                return NotFound();
            }

            locationGallery.IsPrimary = model.IsPrimary;
            locationGallery.ImageCaption = model.ImageCaption;
            locationGallery.LocationID = model.LocationID;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                locationGallery.ImageUrl = await SaveGallery(model.ImageFile);
            }

            _context.Entry(locationGallery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationGalleryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = urlService == null ? "dashboard" : urlService?.RequestUrl?.Url;

            var url = Url.Action(nameof(GetGalleriesByLocationId), new { locationId = locationGallery.LocationID });

            // Return both locationGallery and requestUrl
            return Created(url, new { locationGallery, requestUrl });
        }

        // DELETE: api/LocationGallery/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteLocationGallery(int id)
        {
            var locationGallery = await _context.LocationGalleries.FindAsync(id);
            if (locationGallery == null)
            {
                return NotFound();
            }

            _context.LocationGalleries.Remove(locationGallery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<string> SaveGallery(IFormFile imageFile)
        {
            var fileExtension = Path.GetExtension(imageFile.FileName);
            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(imageFile.FileName)}_{Guid.NewGuid()}{fileExtension}";
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Uploads", "Locations");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return Path.Combine("Uploads", "Locations", uniqueFileName);
        }

        private bool LocationGalleryExists(int id)
        {
            return _context.LocationGalleries.Any(e => e.LocationGalleryID == id);
        }

        public static string RemoveLastSegment(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }

            url = url.TrimStart('/');

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