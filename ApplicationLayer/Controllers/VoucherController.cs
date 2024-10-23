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
    public class VoucherController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public VoucherController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }


        #region Voucher

        [HttpPost("add")]
        public async Task<IActionResult> AddTourVoucher([FromForm] TourVoucherInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? voucherUrl = null;

            if (model.VoucherFile != null)
            {
                voucherUrl = await SaveVoucher(model.VoucherFile);
            }

            var tourVoucher = new TourVoucher
            {
                TourVoucherCode = model.TourVoucherCode,
                VoucherUrl = voucherUrl
            };

            _context.TourVouchers.Add(tourVoucher);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Tour voucher added successfully.",
                tourVoucherID = tourVoucher.TourVoucherID,
                voucherUrl = voucherUrl
            });
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetTourVouchers()
        {
            var tourVouchers = await _context.TourVouchers
                .Select(tv => new
                {
                    tv.TourVoucherID,
                    tv.TourVoucherCode,
                    tv.VoucherUrl
                })
                .ToListAsync();

            if (!tourVouchers.Any())
            {
                return NotFound(new { success = false, message = "No tour vouchers found." });
            }

            return Ok(tourVouchers);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetTourVoucherById(int id)
        {
            var tourVoucher = await _context.TourVouchers
                .Where(tv => tv.TourVoucherID == id)
                .Select(tv => new
                {
                    tv.TourVoucherID,
                    tv.TourVoucherCode,
                    tv.VoucherUrl
                })
                .FirstOrDefaultAsync();

            if (tourVoucher == null)
            {
                return NotFound(new { success = false, message = "Tour voucher not found." });
            }

            return Ok(tourVoucher);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateTourVoucher(int id, [FromForm] TourVoucherInsertModel model)
        {
            var tourVoucher = await _context.TourVouchers.FindAsync(id);
            if (tourVoucher == null)
            {
                return NotFound(new { success = false, message = "Tour voucher not found." });
            }

            tourVoucher.TourVoucherCode = model.TourVoucherCode;

            if (model.VoucherFile != null)
            {
                if (!string.IsNullOrEmpty(tourVoucher.VoucherUrl))
                {
                    var oldFilePath = Path.Combine(_hostEnvironment.WebRootPath, tourVoucher.VoucherUrl);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                tourVoucher.VoucherUrl = await SaveVoucher(model.VoucherFile);
            }

            _context.TourVouchers.Update(tourVoucher);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Tour voucher updated successfully.", tourVoucherID = tourVoucher.TourVoucherID });
        }

        

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTourVoucher(int id)
        {
            var tourVoucher = await _context.TourVouchers.FindAsync(id);
            if (tourVoucher == null)
            {
                return NotFound(new { success = false, message = "Tour voucher not found." });
            }

            if (!string.IsNullOrEmpty(tourVoucher.VoucherUrl))
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Vouchers");
                var filePath = Path.Combine(uploadsFolder, Path.GetFileName(tourVoucher.VoucherUrl));

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.TourVouchers.Remove(tourVoucher);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Tour voucher deleted successfully." });
        }

        private async Task<string> SaveVoucher(IFormFile imageFile)
        {
            var fileExtension = Path.GetExtension(imageFile.FileName);

            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(imageFile.FileName)}_{Guid.NewGuid()}{fileExtension}";


            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Uploads", "Voucher");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return Path.Combine($"Uploads/Voucher/{uniqueFileName}");
        }


        #endregion
    }
}
