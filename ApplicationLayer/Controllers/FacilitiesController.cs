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
    public class FacilitiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FacilitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Facilities
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<FacilityOutputModel>>> GetFacilities()
        {
            var facilities = await _context.Facilities
                .Select(f => new FacilityOutputModel
                {
                    FacilityID = f.FacilityID,
                    FacilityName = f.FacilityName,
                    Description = f.Description,
                    IsAvailable = f.IsAvailable
                })
                .ToListAsync();

            return Ok(facilities);
        }

        // GET: api/Facilities/5
        [HttpGet("get/{id}")]
        public async Task<ActionResult<FacilityOutputModel>> GetFacility(int id)
        {
            var facility = await _context.Facilities
                .Where(f => f.FacilityID == id)
                .Select(f => new FacilityOutputModel
                {
                    FacilityID = f.FacilityID,
                    FacilityName = f.FacilityName,
                    Description = f.Description,
                    IsAvailable = f.IsAvailable
                })
                .FirstOrDefaultAsync();

            if (facility == null)
            {
                return NotFound();
            }

            return Ok(facility);
        }

        // POST: api/Facilities
        [HttpPost("add")]
        public async Task<ActionResult<FacilityOutputModel>> CreateFacility(FacilityInputModel inputModel)
        {
            var facility = new Facility
            {
                FacilityName = inputModel.FacilityName,
                Description = inputModel.Description,
                IsAvailable = inputModel.IsAvailable
            };

            _context.Facilities.Add(facility);
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
                requestUrl = urlService?.RequestUrl?.Url;
            }

            var outputModel = new FacilityOutputModel
            {
                FacilityID = facility.FacilityID,
                FacilityName = facility.FacilityName,
                Description = facility.Description,
                IsAvailable = facility.IsAvailable
            };

            var url = Url.Action(nameof(GetFacility), new { id = outputModel.FacilityID });
            return Created(url, new { facility, requestUrl });
        }

        // PUT: api/Facilities/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateFacility(int id, FacilityInputModel inputModel)
        {
            var facility = await _context.Facilities.FindAsync(id);

            if (facility == null)
            {
                return NotFound();
            }

            facility.FacilityName = inputModel.FacilityName;
            facility.Description = inputModel.Description;
            facility.IsAvailable = inputModel.IsAvailable;

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
                requestUrl = urlService?.RequestUrl?.Url;
            }

            return NoContent();
        }

        // DELETE: api/Facilities/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFacility(int id)
        {
            var facility = await _context.Facilities.FindAsync(id);

            if (facility == null)
            {
                return NotFound();
            }

            _context.Facilities.Remove(facility);
            await _context.SaveChangesAsync();

            return NoContent();
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

