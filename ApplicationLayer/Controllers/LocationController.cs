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
    public class LocationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Location
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocations()
        {
            return await _context.Locations
                .Include(l => l.State)
                .Include(l => l.LocationGalleries)
                .Include(l => l.Hotels)
                .Select(l => new LocationDTO
                {
                    LocationID = l.LocationID,
                    LocationName = l.LocationName,
                    StateName = l.State.StateName,
                    CountryName = l.State.Country.CountryName,
                    LocationGalleries = l.LocationGalleries.Select(g => g.ImageUrl).ToList(),
                    HotelNames = l.Hotels.Select(h => h.HotelName).ToList()
                })
                .ToListAsync();
        }

        // GET: api/Location/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var location = await _context.Locations
                .Include(l => l.State)
                .Include(l => l.LocationGalleries)
                .Include(l => l.Hotels)
                .FirstOrDefaultAsync(l => l.LocationID == id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        // POST: api/Location
        [HttpPost("add")]
        public async Task<ActionResult> PostLocation(LocationInsertModel model)
        {
            var location = new Location
            {
                LocationName = model.LocationName,
                LocationDescription = model.LocationDescription,
                StateID = model.StateID
            };

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                  .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                  .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = urlService == null ? "dashboard" : urlService?.RequestUrl?.Url;

            var url = Url.Action(nameof(GetLocation), new { id = location.LocationID });

            // Return both location and requestUrl
            return Created(url, new { location, requestUrl });
        }


        // PUT: api/Location/{id}
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutLocation(int id, LocationInsertModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            location.LocationName = model.LocationName;
            location.LocationDescription = model.LocationDescription;
            location.StateID = model.StateID;

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

            var url = Url.Action(nameof(GetLocation), new { id = location.LocationID });

            // Return both location and requestUrl
            return Created(url, new { location, requestUrl });
        }

        // DELETE: api/Location/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.LocationID == id);
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