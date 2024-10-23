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
    public class TransportProviderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransportProviderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TransportProvider/get-all
        [HttpGet("get")]
        public async Task<IActionResult> GetAllTransportProviders()
        {
            var providers = await _context.TransportProviders
                .Select(tp => new
                {
                    tp.TransportProviderID,
                    tp.Name,
                    tp.CompanyName,
                    tp.ContactNumber,
                    tp.Address,
                    tp.IsVerified
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                data = providers
            });
        }



        // POST: api/TransportProvider/add
        [HttpPost("add")]
        public async Task<IActionResult> AddTransportProvider([FromBody] TransportProviderInsertModel model, string? customUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transportProvider = new TransportProvider
            {
                Name = model.Name,
                CompanyName = model.CompanyName,
                ContactNumber = model.ContactNumber,
                Address = model.Address,
                IsVerified = model.IsVerified
            };

            await _context.TransportProviders.AddAsync(transportProvider);
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

            //  var url = customUrl ?? $"api/TransportProvider/get-by-id/{transportProvider.TransportProviderID}";

            return Ok(new
            {
                success = true,
                message = "Transport provider added successfully.",
                transportProviderID = transportProvider.TransportProviderID,
                url = requestUrl
            });
        }
        // GET: api/TransportProvider/get-by-id/{id}
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetTransportProviderById(int id)
        {
            var provider = await _context.TransportProviders
                .Where(tp => tp.TransportProviderID == id)
                .Select(tp => new
                {
                    tp.TransportProviderID,
                    tp.Name,
                    tp.CompanyName,
                    tp.ContactNumber,
                    tp.Address,
                    tp.IsVerified
                })
                .FirstOrDefaultAsync();

            if (provider == null)
            {
                return NotFound(new { success = false, message = "Transport provider not found." });
            }

            return Ok(new
            {
                success = true,
                data = provider
            });
        }

        // PUT: api/TransportProvider/update/{id}
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateTransportProvider(int id, [FromBody] TransportProviderInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProvider = await _context.TransportProviders.FindAsync(id);

            if (existingProvider == null)
            {
                return NotFound(new { success = false, message = "Transport provider not found." });
            }

            existingProvider.Name = model.Name;
            existingProvider.CompanyName = model.CompanyName;
            existingProvider.ContactNumber = model.ContactNumber;
            existingProvider.Address = model.Address;
            existingProvider.IsVerified = model.IsVerified;

            _context.TransportProviders.Update(existingProvider);
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

            return Ok(new
            {
                success = true,
                message = "Transport provider updated successfully.",
                transportProviderID = existingProvider.TransportProviderID,
                url = requestUrl,
            });
        }

        // DELETE: api/TransportProvider/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTransportProvider(int id)
        {
            var provider = await _context.TransportProviders.FindAsync(id);

            if (provider == null)
            {
                return NotFound(new { success = false, message = "Transport provider not found." });
            }

            _context.TransportProviders.Remove(provider);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Transport provider deleted successfully."
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
