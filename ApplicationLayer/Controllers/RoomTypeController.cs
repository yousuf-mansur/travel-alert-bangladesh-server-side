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
    public class RoomTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RoomTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTypeDTO>>> GetRoomTypes()
        {
            var roomTypes = await _context.RoomTypes.ToListAsync();
            return roomTypes.Select(rt => new RoomTypeDTO { RoomTypeID = rt.RoomTypeID, TypeName = rt.TypeName }).ToList();
        }

        // GET: api/RoomTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomTypeDTO>> GetRoomType(int id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return new RoomTypeDTO { RoomTypeID = roomType.RoomTypeID, TypeName = roomType.TypeName };
        }

        // POST: api/RoomTypes
        [HttpPost("add/type")]
        public async Task<ActionResult<RoomTypeDTO>> CreateRoomType([FromForm] RoomTypeDTO roomTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RoomType roomType = new RoomType
            {
                TypeName = roomTypeDTO.TypeName
            };

            _context.RoomTypes.Add(roomType);
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
                requestUrl = urlService?.RequestUrl?.Url ?? "dashboard";
            }




            // Return only id and url in the response
            return CreatedAtAction(nameof(GetRoomType), new { id = roomType.RoomTypeID }, new { id = roomType.RoomTypeID, url = requestUrl });
        }

        // PUT: api/RoomTypes/5
        [HttpPut("update/type/{id}")]
        public async Task<IActionResult> UpdateRoomType(int id, [FromForm] RoomTypeDTO roomTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }

            roomType.TypeName = roomTypeDTO.TypeName;

            _context.RoomTypes.Update(roomType);
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

            return Ok(new { RoomTypeID = roomType.RoomTypeID, Url = requestUrl });
        }


        // DELETE: api/RoomTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomType(int id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }

            _context.RoomTypes.Remove(roomType);
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
