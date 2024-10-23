using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;
using DataAccessLayer.DTOs.InputModels;
using DataAccessLayer.DTOs.OutputModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Data;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CountryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Country
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _context.Countries.Include(c => c.States).ToListAsync();
        }

        // GET: api/Country/{id}
        [HttpGet("get/{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.Include(c => c.States)
                .FirstOrDefaultAsync(c => c.CountryID == id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // POST: api/Country
        [HttpPost("add")]
        public async Task<ActionResult<Country>> PostCountry(CountryInsertModel model)
        {
            var country = new Country
            {
                CountryName = model.CountryName
            };

            _context.Countries.Add(country);
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

            var url = Url.Action(nameof(GetCountry), new { id = country.CountryID, url = requestUrl });

            // Return both the URL and the requestUrl in the response
            return Created(url, new { country, requestUrl });
        }


        // PUT: api/Country/{id}
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutCountry(int id, CountryInsertModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            country.CountryName = model.CountryName;

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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
                Url = requestUrl,
            });
        }

        // DELETE: api/Country/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryID == id);
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
