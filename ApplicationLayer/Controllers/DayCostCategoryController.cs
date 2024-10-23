using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;
using DataAccessLayer.DTOs.InputModels;


namespace ApplicationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DayCostCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DayCostCategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DayCostCategory
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _context.DayCostCategory
                .Select(c => new
                {
                    c.DayCostCategoryID,
                    c.DayCostCategoryName,
                    c.CreatedAt,
                    c.UpdatedAt
                })
                .ToListAsync();

            return Ok(new { success = true, data = categories });
        }

        // GET: api/DayCostCategory/{id}
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _context.DayCostCategory
                .Where(c => c.DayCostCategoryID == id)
                .Select(c => new
                {
                    c.DayCostCategoryID,
                    c.DayCostCategoryName,
                    c.CreatedAt,
                    c.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound(new { success = false, message = "DayCostCategory not found." });
            }

            return Ok(new { success = true, data = category });
        }

        // POST: api/DayCostCategory/insert
        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] DayCostCategoryInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = new DayCostCategory
            {
                DayCostCategoryName = model.DayCostCategoryName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.DayCostCategory.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "DayCostCategory added successfully." });
        }

        // PUT: api/DayCostCategory/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DayCostCategoryInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.DayCostCategory.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { message = "DayCostCategory not found." });
            }

            category.DayCostCategoryName = model.DayCostCategoryName;
            category.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "DayCostCategory updated successfully." });
        }

        // DELETE: api/DayCostCategory/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.DayCostCategory.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { message = "DayCostCategory not found." });
            }

            _context.DayCostCategory.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "DayCostCategory deleted successfully." });
        }
    }

}
