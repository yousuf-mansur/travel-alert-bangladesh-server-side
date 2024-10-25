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
    public class PackageCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;


        public PackageCategoryController(AppDbContext context)
        {
            _context = context;

        }

        [HttpGet("get/all/categories")]
        public async Task<IActionResult> GetAllPackageCategories([FromQuery] string? customUrl = null)
        {
            var packageCategories = await _context.PackageCategories
                .Select(pc => new
                {
                    pc.PackageCategoryID,
                    pc.CategoryName,
                    pc.Description,
                    pc.CreatedAt,
                    pc.UpdatedAt
                })
                .ToListAsync();

            if (packageCategories == null || !packageCategories.Any())
            {
                return NotFound("No package categories found.");
            }

            var url = customUrl ?? "getcategories";

            return Ok(new
            {
                success = true,
                message = "Package categories retrieved successfully.",
                url,
                categories = packageCategories
            });
        }


        [HttpPost("add/category")]
        public async Task<IActionResult> CreatePackageCategory([FromBody] PackageCategoryInsertModel model, [FromQuery] string? customUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageCategory = new PackageCategory
            {
                CategoryName = model.CategoryName,
                Description = model.Description,
                CreatedAt = DateTime.Now, 
                UpdatedAt = DateTime.Now 
            };

            _context.PackageCategories.Add(packageCategory);
            await _context.SaveChangesAsync();

            var url = customUrl ?? "getcategory";

            return CreatedAtAction(nameof(CreatePackageCategory), new { categoryId = packageCategory.PackageCategoryID }, new
            {
                success = true,
                message = "Package category created successfully.",
                categoryId = packageCategory.PackageCategoryID,
                url
            });
        }

        [HttpGet("get/category/{categoryId}")]
        public async Task<IActionResult> GetPackageCategoryById(int categoryId, [FromQuery] string? customUrl = null)
        {
            var packageCategory = await _context.PackageCategories
                .Where(pc => pc.PackageCategoryID == categoryId)
                .Select(pc => new
                {
                    pc.PackageCategoryID,
                    pc.CategoryName,
                    pc.Description,
                    pc.CreatedAt,
                    pc.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (packageCategory == null)
            {
                return NotFound($"Package category with ID {categoryId} not found.");
            }
 
            var url = customUrl ?? "getcategorybyid";

            return Ok(new
            {
                success = true,
                message = "Package category retrieved successfully.",
                url,
                category = packageCategory
            });
        }


        [HttpPut("update/category/{categoryId}")]
        public async Task<IActionResult> UpdatePackageCategory(int categoryId, [FromBody] PackageCategoryInsertModel model, [FromQuery] string? customUrl = null)
        {
            var packageCategory = await _context.PackageCategories.FindAsync(categoryId);

            if (packageCategory == null)
            {
                return NotFound("Category not found.");
            }

            packageCategory.CategoryName = model.CategoryName;
            packageCategory.Description = model.Description;
            packageCategory.UpdatedAt = DateTime.Now; 

            _context.PackageCategories.Update(packageCategory);
            await _context.SaveChangesAsync();

            var url = customUrl ?? "getcategory";

            return Ok(new
            {
                success = true,
                message = "Package category updated successfully.",
                categoryId = packageCategory.PackageCategoryID,
                url
            });
        }



    }
}
