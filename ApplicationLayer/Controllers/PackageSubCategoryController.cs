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
    public class PackageSubCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;


        public PackageSubCategoryController(AppDbContext context)
        {
            _context = context;

        }


        [HttpGet("get-all-subcategories")]
        public async Task<IActionResult> GetAllSubCategories()
        {
            var subCategories = await _context.PackageSubCategories
                .Select(s => new
                {
                    s.PackageSubCategoryID,
                    s.SubCategoryName,
                    s.Description,
                    s.CreatedAt,
                    s.UpdatedAt,
                    s.PackageCategoryID
                })
                .ToListAsync();

            return Ok(subCategories);
        }

        [HttpPost("add-subcategory")]
        public async Task<IActionResult> CreateSubCategory([FromBody] PackageSubCategoryInsertModel model, [FromQuery] string? customUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subCategory = new PackageSubCategory
            {
                PackageCategoryID = model.PackageCategoryID,
                SubCategoryName = model.SubCategoryName,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.PackageSubCategories.Add(subCategory);
            await _context.SaveChangesAsync();

            var url = customUrl ?? "getsubcategory";

            return CreatedAtAction(nameof(CreateSubCategory), new { subCategoryId = subCategory.PackageSubCategoryID }, new
            {
                success = true,
                message = "Subcategory created successfully.",
                subCategoryId = subCategory.PackageSubCategoryID,
                url
            });
        }

        [HttpGet("get-subcategory/{id}")]
        public async Task<IActionResult> GetSubCategoryById(int id)
        {
            var subCategory = await _context.PackageSubCategories
                .Where(s => s.PackageSubCategoryID == id)
                .Select(s => new
                {
                    s.PackageSubCategoryID,
                    s.SubCategoryName,
                    s.Description,
                    s.CreatedAt,
                    s.UpdatedAt,
                    s.PackageCategoryID
                })
                .FirstOrDefaultAsync();

            if (subCategory == null)
            {
                return NotFound("Subcategory not found.");
            }

            return Ok(subCategory);
        }

        

        [HttpPut("update-subcategory/{id}")]
        public async Task<IActionResult> UpdateSubCategory(int id, [FromBody] PackageSubCategoryInsertModel model)
        {
            var subCategory = await _context.PackageSubCategories.FindAsync(id);

            if (subCategory == null)
            {
                return NotFound("Subcategory not found.");
            }

            subCategory.PackageCategoryID = model.PackageCategoryID;
            subCategory.SubCategoryName = model.SubCategoryName;
            subCategory.Description = model.Description;
            subCategory.UpdatedAt = DateTime.Now;

            _context.PackageSubCategories.Update(subCategory);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Subcategory updated successfully.",
                subCategoryId = subCategory.PackageSubCategoryID
            });
        }

        [HttpGet("get-subcategories-by-category/{categoryId}")]
        public async Task<IActionResult> GetSubCategoriesByCategory(int categoryId)
        {
            var subCategories = await _context.PackageSubCategories
                .Where(s => s.PackageCategoryID == categoryId)
                .Select(s => new
                {
                    s.PackageSubCategoryID,
                    s.SubCategoryName,
                    s.Description,
                    s.PackageCategory.CategoryName,
                    s.PackageCategory.PackageCategoryID
                })
                .ToListAsync();

            if (subCategories == null || !subCategories.Any())
            {
                return NotFound(new
                {
                    success = false,
                    message = "No subcategories found for the given category."
                });
            }

            return Ok(subCategories);
        }


    }
}
