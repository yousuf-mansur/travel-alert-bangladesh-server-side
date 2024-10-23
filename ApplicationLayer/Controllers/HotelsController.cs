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
    public class HotelsController : ControllerBase
    {
        private readonly AppDbContext _context;



        public HotelsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetHotels()
        {
            var hotels = _context.Hotels

                .Include(h => h.HotelImages)
                .Include(h => h.HotelFacilities)
                .Include(h => h.Rooms)
                .Select(h => new
                {
                    h.HotelID,
                    h.HotelName,
                    h.Description,
                    h.Address,
                    h.StarRating,
                    h.ContactInfo,
                    h.LocationID,
                    h.HotelCode,
                    h.HotelFacilities,
                    h.HotelImages,

                });

            return Ok(hotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{ID}")]
        public IActionResult GetHotel(int ID)
        {
            var hotel = _context.Hotels

                .Include(h => h.HotelImages)
                .Include(h => h.HotelFacilities)
                .Include(h => h.Rooms)
                .Select(h => new
                {
                    h.HotelID,
                    h.HotelName,
                    h.Description,
                    h.Address,
                    h.StarRating,
                    h.ContactInfo,
                    h.LocationID,
                    h.HotelCode,
                    h.HotelFacilities,
                    h.HotelImages,

                })
                .FirstOrDefault(h => h.HotelID == ID);

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        // POST api/Hotels
        [HttpPost("add/hotel")]
        public async Task<ActionResult<int>> CreateHotel([FromBody] FacilityWiseHotel facilityWiseHotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create a new Hotel entity and map fields from DTO
            Hotel hotel = new Hotel
            {
                HotelName = facilityWiseHotel.HotelName,
                Description = facilityWiseHotel.Description,
                StarRating = facilityWiseHotel.StarRating,
                Address = facilityWiseHotel.Address,
                ContactInfo = facilityWiseHotel.ContactInfo,
                HotelCode = facilityWiseHotel.HotelCode,
                LocationID = facilityWiseHotel.LocationId
            };

            // Add the hotel to the database context
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            // Add hotel facilities if provided in the DTO
            foreach (var hotelFacility in facilityWiseHotel.HotelFacilities)
            {
                HotelFacility hf = new HotelFacility
                {
                    HotelID = hotel.HotelID,
                    FacilityID = hotelFacility.FacilityID,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow
                };
                _context.HotelFacilities.Add(hf);
            }

            await _context.SaveChangesAsync();

            // Construct the URL based on the current request
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
                requestUrl = urlService.RequestUrl?.Url ?? "dashboard";
            }

            // Return response containing `id` and `url`
            return Ok(new { id = hotel.HotelID, url = requestUrl });
        }


        // PUT api/Hotels/5
        [HttpPut("update/hotel/{id}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] FacilityWiseHotel facilityWiseHotel)
        {
            // Check for ID mismatch between request URL and DTO
            if (id != facilityWiseHotel.HotelId)
            {
                return BadRequest();
            }

            // Validate the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Fetch the hotel with related entities
            var hotel = await _context.Hotels
                .Include(h => h.HotelFacilities)
                .ThenInclude(hf => hf.Facility)
                .Include(h => h.HotelImages)
                .FirstOrDefaultAsync(h => h.HotelID == id);

            if (hotel == null)
            {
                return NotFound();
            }

            // Update hotel properties with the values from the DTO
            hotel.HotelName = facilityWiseHotel.HotelName;
            hotel.Description = facilityWiseHotel.Description;
            hotel.StarRating = facilityWiseHotel.StarRating;
            hotel.Address = facilityWiseHotel.Address;
            hotel.ContactInfo = facilityWiseHotel.ContactInfo;
            hotel.HotelCode = facilityWiseHotel.HotelCode;
            hotel.LocationID = facilityWiseHotel.LocationId;

            // Mark the entity as modified
            _context.Entry(hotel).State = EntityState.Modified;

            // Update or add hotel facilities
            foreach (var hotelFacility in facilityWiseHotel.HotelFacilities)
            {
                var existingHotelFacility = await _context.HotelFacilities
                    .FirstOrDefaultAsync(hf => hf.HotelID == hotel.HotelID && hf.FacilityID == hotelFacility.FacilityID);

                if (existingHotelFacility == null)
                {
                    // Add new facility if not found
                    HotelFacility hf = new HotelFacility
                    {
                        HotelID = hotel.HotelID,
                        FacilityID = hotelFacility.FacilityID,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };
                    _context.HotelFacilities.Add(hf);
                }
                else
                {
                    // Update the existing facility timestamp
                    existingHotelFacility.UpdatedOn = DateTime.UtcNow;
                }
            }

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
                requestUrl = urlService.RequestUrl?.Url ?? "dashboard";
            }

            // Corrected return statement with id and url
            return Ok(new { id = hotel.HotelID, url = requestUrl });
        }

        // DELETE api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels
                .Include(h => h.HotelFacilities)
                .ThenInclude(hf => hf.Facility)
                .Include(h => h.HotelImages)
                .FirstOrDefaultAsync(h => h.HotelID == id);

            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return Ok("Deleted succesfully");
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

