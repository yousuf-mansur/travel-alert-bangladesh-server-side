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
    public class UrlServiceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UrlServiceController(AppDbContext context)
        {
            _context = context;
        }

        // URL Service Endpoints

        // GET: api/UrlService/urlservices
        // GET: api/UrlService
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UrlServiceDTO>>> GetUrlServices()
        {
            var urlServices = await _context.UrlServices
                .Include(us => us.RequestUrl)
                .Include(us => us.CurrentUrl)
                .ToListAsync();

            var result = urlServices.Select(us => new UrlServiceDTO
            {
                UrlServiceId = us.UrlServiceId,
                CurrentUrlId = us.CurrentUrl.CurrentUrlId,
                RequestUrlId = us.RequestUrl.RequestUrlId,
                Description = us.Description
            }).ToList();

            return Ok(result);
        }

        // GET: api/UrlService/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UrlServiceDTO>> GetUrlService(int id)
        {
            var urlService = await _context.UrlServices
                .Include(us => us.RequestUrl)
                .Include(us => us.CurrentUrl)
                .FirstOrDefaultAsync(us => us.UrlServiceId == id);

            if (urlService == null)
            {
                return NotFound();
            }

            var result = new UrlServiceDTO
            {
                UrlServiceId = urlService.UrlServiceId,
                CurrentUrlId = urlService.CurrentUrl.CurrentUrlId,
                RequestUrlId = urlService.RequestUrl.RequestUrlId,
                Description = urlService.Description
            };

            return Ok(result);
        }

        // POST: api/UrlService
        [HttpPost]
        public async Task<ActionResult<UrlServiceDTO>> PostUrlService(UrlServiceDTO urlServiceDTO)
        {
            var requestUrl = await _context.RequestUrls.FindAsync(urlServiceDTO.RequestUrlId);
            var currentUrl = await _context.CurrentUrls.FindAsync(urlServiceDTO.CurrentUrlId);

            if (requestUrl == null || currentUrl == null)
            {
                return BadRequest("Invalid RequestUrlId or CurrentUrlId.");
            }

            var urlService = new UrlService
            {
                CurrentUrlId = urlServiceDTO.CurrentUrlId,
                RequestUrlId = urlServiceDTO.RequestUrlId,
                Description = urlServiceDTO.Description
            };

            _context.UrlServices.Add(urlService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUrlService", new { id = urlService.UrlServiceId }, urlServiceDTO);
        }

        // PUT: api/UrlService/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUrlService(int id, UrlServiceDTO urlServiceDTO)
        {
            if (id != urlServiceDTO.UrlServiceId)
            {
                return BadRequest();
            }

            var urlService = await _context.UrlServices.FindAsync(id);
            if (urlService == null)
            {
                return NotFound();
            }

            urlService.CurrentUrlId = urlServiceDTO.CurrentUrlId;
            urlService.RequestUrlId = urlServiceDTO.RequestUrlId;
            urlService.Description = urlServiceDTO.Description;

            _context.Entry(urlService).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UrlService/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUrlService(int id)
        {
            var urlService = await _context.UrlServices.FindAsync(id);
            if (urlService == null)
            {
                return NotFound();
            }

            _context.UrlServices.Remove(urlService);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("requesturls")]
        public async Task<ActionResult<IEnumerable<RequestUrlDTO>>> GetAllRequestUrls()
        {
            var requestUrls = await _context.RequestUrls.ToListAsync();

            var result = requestUrls.Select(r => new RequestUrlDTO
            {
                RequestUrlId = r.RequestUrlId,
                Url = r.Url,
                UrlName = r.UrlName
            }).ToList();

            return Ok(result);
        }

        // GET: api/CurrentUrl
        [HttpGet("currenturls")]
        public async Task<ActionResult<IEnumerable<CurrentUrlDTO>>> GetAllCurrentUrls()
        {
            var currentUrls = await _context.CurrentUrls.ToListAsync();

            var result = currentUrls.Select(c => new CurrentUrlDTO
            {
                CurrentUrlId = c.CurrentUrlId,
                Url = c.Url,
                Title = c.Title
            }).ToList();

            return Ok(result);
        }
    }
}
