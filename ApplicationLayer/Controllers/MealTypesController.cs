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
    public class MealTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MealTypesController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/MealTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealType>>> GetMealTypes()
        {
            return await _context.MealTypes.ToListAsync();
        }
        // GET: api/MealTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MealType>> GetMealType(int id)
        {
            var mealType = await _context.MealTypes.FindAsync(id);

            if (mealType == null)
            {
                return NotFound();
            }

            return mealType;
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutMealType(int id, MealType mealType)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            _context.Entry(mealType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealTypeExists(id))
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


            var requestUrl = urlService == null ? "dashboard" :
                urlService?.RequestUrl?.Url;

            var url = Url.Action(nameof(GetMealType), new { id = mealType.MealTypeID });

            return Created(url, new { mealType, requestUrl });
        }

        [HttpPost("add")]
        public async Task<ActionResult<MealType>> PostMealType(MealType mealType)
        {
            _context.MealTypes.Add(mealType);
            await _context.SaveChangesAsync();

            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);


            var urlService = await _context.UrlServices
                .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());


            var requestUrl = urlService == null ? "dashboard" :
                urlService?.RequestUrl?.Url;

            var url = Url.Action(nameof(GetMealType), new { id = mealType.MealTypeID });


            return Created(url, new { mealType, requestUrl });
        }

        // DELETE: api/MealTypes/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteMealType(int id)
        {
            var mealType = await _context.MealTypes.FindAsync(id);
            if (mealType == null)
            {
                return NotFound();
            }

            _context.MealTypes.Remove(mealType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MealTypeExists(int id)
        {
            return _context.MealTypes.Any(e => e.MealTypeID == id);
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
