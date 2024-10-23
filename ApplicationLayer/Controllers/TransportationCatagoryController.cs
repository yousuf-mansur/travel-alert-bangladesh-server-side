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
    public class TransportationCatagoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransportationCatagoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TransportationCatagory/get
        [HttpGet("get")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.TransportationCatagories
                .Select(c => new
                {
                    c.TransportationCatagoryID,
                    c.TransportationCatagoryName
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                data = categories
            });
        }

        // GET: api/TransportationCatagory/get/{id}
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _context.TransportationCatagories
                .Where(c => c.TransportationCatagoryID == id)
                .Select(c => new
                {
                    c.TransportationCatagoryID,
                    c.TransportationCatagoryName
                })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound(new { success = false, message = "Transportation category not found." });
            }

            return Ok(new
            {
                success = true,
                data = category
            });
        }

        // POST: api/TransportationCatagory/add
        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] TransportationCatagoryInsertModel model, string? customUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCategory = new TransportationCatagory
            {
                TransportationCatagoryName = model.TransportationCatagoryName
            };

            await _context.TransportationCatagories.AddAsync(newCategory);
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

            //var url = customUrl ?? "get-all"; // Custom URL or default

            return Ok(new
            {
                success = true,
                message = "Transportation category added successfully.",
                categoryId = newCategory.TransportationCatagoryID,
                url = requestUrl // Include custom URL in response
            });
        }

        // PUT: api/TransportationCatagory/update-category/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] TransportationCatagoryInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.TransportationCatagories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { success = false, message = "Transportation category not found." });
            }

            category.TransportationCatagoryName = model.TransportationCatagoryName;

            _context.TransportationCatagories.Update(category);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Transportation category updated successfully."
            });
        }

        // DELETE: api/TransportationCatagory/delete-category/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.TransportationCatagories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { success = false, message = "Transportation category not found." });
            }

            _context.TransportationCatagories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Transportation category deleted successfully."
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
