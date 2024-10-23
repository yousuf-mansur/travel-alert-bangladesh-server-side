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
    public class FoodItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FoodItemsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/FoodItems
        [HttpGet]
        public async Task<IActionResult> GetAllFoodItems()
        {

            var foodItems = await _context.FoodItems
                .Select(item => new FoodItemOutputModel
                {
                    FoodItemID = item.FoodItemID,
                    ItemName = item.ItemName,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt

                })
                .ToListAsync();

            if (!foodItems.Any())
            {
                return NotFound(new { success = false, message = "No food items found." });
            }

            return Ok(new { success = true, data = foodItems });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoodItem(int id)
        {
            var foodItem = await _context.FoodItems
                .Where(item => item.FoodItemID == id)
                .Select(item => new FoodItemOutputModel
                {
                    FoodItemID = item.FoodItemID,
                    ItemName = item.ItemName,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (foodItem == null)
            {
                return NotFound(new { success = false, message = "Food item not found." });
            }

            return Ok(new { success = true, data = foodItem });
        }


        [HttpPost("add")]
        public async Task<IActionResult> CreateFoodItem([FromBody] FoodItemInputModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var foodItem = new FoodItem
            {
                ItemName = model.ItemName,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
            };


            _context.FoodItems.Add(foodItem);
            await _context.SaveChangesAsync();


            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                           .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                           .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());


            var requestUrl = urlService == null ? "dashboard" :
                urlService?.RequestUrl?.Url;

            var url = Url.Action(nameof(GetFoodItem), new { id = foodItem.FoodItemID });


            return Created(url, new { foodItem, requestUrl });
        }




        // PUT: api/FoodItems/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutFoodItem(int id, FoodItemInputModel foodItemModel)
        {

            if (id != foodItemModel.FoodItemID)
            {
                return BadRequest();
            }


            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            foodItem.ItemName = foodItemModel.ItemName;
            foodItem.CreatedAt = foodItemModel.CreatedAt;
            foodItem.UpdatedAt = foodItemModel.UpdatedAt;


            _context.Entry(foodItem).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodItemExists(id))
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
                .Include(u => u.RequestUrl).Include(u=>u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = urlService == null ? "dashboard" :
                urlService?.RequestUrl?.Url;


            var url = Url.Action(nameof(GetAllFoodItems), new { id = foodItem.FoodItemID });

            return Created(url, new { foodItem, requestUrl });
        }


        // DELETE: api/FoodItems/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFoodItem(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodItemExists(int id)
        {
            return _context.FoodItems.Any(e => e.FoodItemID == id);
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
