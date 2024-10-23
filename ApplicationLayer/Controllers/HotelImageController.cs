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
    public class HotelImageController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HotelImageController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("add/image")]
        public async Task<ActionResult<InputImage>> CreateHotelImage([FromForm] InputImage inputImage)
        {
            // Check if the incoming model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string uniqueFileName = null;

            // If an image file is provided, process the upload
            if (inputImage.ImageProfile != null)
            {
                // Define the upload folder path
                string uploadsFolder = Path.Combine(_env.ContentRootPath, "Image");

                // Create the directory if it doesn't exist
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate a unique file name and save the image file
                uniqueFileName = Guid.NewGuid().ToString() + "_" + inputImage.ImageProfile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await inputImage.ImageProfile.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred while uploading the file: {ex.Message}");
                }
            }

            // Create a new HotelImage entity and populate its properties
            HotelImage hotelImage = new HotelImage
            {
                HotelID = inputImage.HotelID,
                ImageUrl = uniqueFileName,
                Caption = inputImage.Caption,
                IsThumbnail = inputImage.IsThumbnail,
                CreatedOn = DateTime.UtcNow
            };

            // Add the new entity to the database and save changes
            _context.HotelImages.Add(hotelImage);
            await _context.SaveChangesAsync();

            // Retrieve the request path and URL
            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            // Get the corresponding URL service record
            var urlService = await _context.UrlServices
                .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            // Determine the request URL or fallback to "dashboard"
            var requestUrl = urlService?.RequestUrl?.Url ?? "dashboard";

            // Return the newly created image ID and URL
            return CreatedAtAction(nameof(GetHotelImage), new { id = hotelImage.HotelImageID }, new { id = hotelImage.HotelImageID, url = requestUrl });
        }


        // PUT api/HotelImage/5
        [HttpPut("update/image/{id}")]
        public async Task<IActionResult> UpdateHotelImage(int id, [FromForm] InputImage inputImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HotelImage hotelImage = await _context.HotelImages.FindAsync(id);
            if (hotelImage == null)
            {
                return NotFound();
            }

            if (inputImage.ImageProfile != null)
            {
                string uploadsFolder = Path.Combine(_env.ContentRootPath, "Image");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + inputImage.ImageProfile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await inputImage.ImageProfile.CopyToAsync(stream);
                }
                hotelImage.ImageUrl = uniqueFileName;
            }
            hotelImage.Caption = inputImage.Caption;
            hotelImage.IsThumbnail = inputImage.IsThumbnail;


            _context.Entry(hotelImage).State = EntityState.Modified;
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


            return Ok(new { id = hotelImage.HotelImageID, url = requestUrl });
        }

        // GET api/HotelImage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Outputimage>> GetHotelImage(int id)
        {
            HotelImage hotelImage = await _context.HotelImages.FindAsync(id);
            if (hotelImage == null)
            {
                return NotFound();
            }

            Outputimage outputImage = new Outputimage
            {
                HotelImageID = hotelImage.HotelImageID,
                ImageUrl = hotelImage.ImageUrl,
                Caption = hotelImage.Caption,
                IsThumbnail = hotelImage.IsThumbnail,
                CreatedOn = hotelImage.CreatedOn,
                HotelID = hotelImage.HotelID
            };

            return outputImage;
        }



        // DELETE api/HotelImage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelImage(int id)
        {
            HotelImage hotelImage = await _context.HotelImages.FindAsync(id);
            if (hotelImage == null)
            {
                return NotFound();
            }

            _context.HotelImages.Remove(hotelImage);
            await _context.SaveChangesAsync();

            return Ok("Deleted Successfully");
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