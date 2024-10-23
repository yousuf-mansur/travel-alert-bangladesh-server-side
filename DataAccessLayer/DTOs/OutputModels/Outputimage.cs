
using Microsoft.AspNetCore.Http;

namespace DataAccessLayer.DTOs.OutputModels
{
    public class Outputimage
    {
        public int HotelID { get; set; }
        public int HotelImageID { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ImageProfile { get; set; }
        public string Caption { get; set; }
        public bool IsThumbnail { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
