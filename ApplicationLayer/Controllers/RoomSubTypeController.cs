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
    public class RoomSubTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomSubTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RoomSubTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomSubTypeDTO>>> GetRoomSubTypes()
        {
            var roomSubTypes = await _context.RoomSubTypes.ToListAsync();
            return roomSubTypes.Select(rst => new RoomSubTypeDTO { RoomSubTypeID = rst.RoomSubTypeID, SubTypeName = rst.SubTypeName }).ToList();
        }

        // GET: api/RoomSubTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomSubTypeDTO>> GetRoomSubType(int id)
        {
            var roomSubType = await _context.RoomSubTypes.FindAsync(id);
            if (roomSubType == null)
            {
                return NotFound();
            }
            return new RoomSubTypeDTO { RoomSubTypeID = roomSubType.RoomSubTypeID, SubTypeName = roomSubType.SubTypeName };
        }

        // POST: api/RoomSubTypes
        [HttpPost("add/sub/type")]
        public async Task<ActionResult<RoomSubTypeDTO>> CreateRoomSubType([FromForm] RoomSubTypeDTO roomSubTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RoomSubType roomSubType = new RoomSubType
            {
                SubTypeName = roomSubTypeDTO.SubTypeName
            };

            _context.RoomSubTypes.Add(roomSubType);
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

            return CreatedAtAction(nameof(GetRoomSubType), new { id = roomSubType.RoomSubTypeID }, new { id = roomSubType.RoomSubTypeID, url = requestUrl });
        }

        // PUT: api/RoomSubTypes/5
        [HttpPut("update-sub-type/{id}")]
        public async Task<IActionResult> UpdateRoomSubType(int id, [FromForm] RoomSubTypeDTO roomSubTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomSubType = await _context.RoomSubTypes.FindAsync(id);
            if (roomSubType == null)
            {
                return NotFound();
            }

            roomSubType.SubTypeName = roomSubTypeDTO.SubTypeName;

            _context.Entry(roomSubType).State = EntityState.Modified;
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
            return Ok(new { RoomSubTypeID = roomSubType.RoomSubTypeID, Url = requestUrl });
        }

        // DELETE: api/RoomSubTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomSubType(int id)
        {
            var roomSubType = await _context.RoomSubTypes.FindAsync(id);
            if (roomSubType == null)
            {
                return NotFound();
            }

            _context.RoomSubTypes.Remove(roomSubType);
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
