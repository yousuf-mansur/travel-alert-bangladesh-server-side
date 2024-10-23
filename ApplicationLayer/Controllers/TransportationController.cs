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
    public class TransportationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransportationController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Transportation/get-all
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllTransportation()
        {
            var transportations = await _context.Transportations
                .Include(t => t.TransportProvider)
                .Select(t => new
                {
                    t.TransportationID,
                    t.IsActive,
                    t.TransportProviderID,
                    TransportProviderName = t.TransportProvider.Name,
                    t.Description
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                data = transportations
            });
        }



        // POST: api/Transportation/add
        [HttpPost("add")]
        public async Task<IActionResult> AddTransportation([FromBody] TransportationInsertModel model, string? customUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transportation = new Transportation
            {
                IsActive = model.IsActive,
                TransportProviderID = model.TransportProviderID,
                Description = model.Description
            };

            await _context.Transportations.AddAsync(transportation);
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

            //var url = customUrl ?? $"api/Transportation/get-by-id/{transportation.TransportationID}";

            return Ok(new
            {
                success = true,
                message = "Transportation added successfully.",
                transportationID = transportation.TransportationID,
                url = requestUrl
            });
        }

        // GET: api/Transportation/get-by-id/{id}
        [HttpGet("getid/{id}")]
        public async Task<IActionResult> GetTransportationById(int id)
        {
            var transportation = await _context.Transportations
                .Include(t => t.TransportProvider)
                .Where(t => t.TransportationID == id)
                .Select(t => new
                {
                    t.TransportationID,
                    t.IsActive,
                    t.TransportProviderID,
                    TransportProviderName = t.TransportProvider.Name,
                    t.Description
                })
                .FirstOrDefaultAsync();

            if (transportation == null)
            {
                return NotFound(new { success = false, message = "Transportation not found." });
            }

            return Ok(new
            {
                success = true,
                data = transportation
            });
        }

        // PUT: api/Transportation/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTransportation(int id, [FromBody] TransportationInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTransportation = await _context.Transportations.FindAsync(id);

            if (existingTransportation == null)
            {
                return NotFound(new { success = false, message = "Transportation not found." });
            }

            existingTransportation.IsActive = model.IsActive;
            existingTransportation.TransportProviderID = model.TransportProviderID;
            existingTransportation.Description = model.Description;

            _context.Transportations.Update(existingTransportation);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Transportation updated successfully.",
                transportationID = existingTransportation.TransportationID
            });
        }

        // DELETE: api/Transportation/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTransportation(int id)
        {
            var transportation = await _context.Transportations.FindAsync(id);

            if (transportation == null)
            {
                return NotFound(new { success = false, message = "Transportation not found." });
            }

            _context.Transportations.Remove(transportation);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Transportation deleted successfully."
            });
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
