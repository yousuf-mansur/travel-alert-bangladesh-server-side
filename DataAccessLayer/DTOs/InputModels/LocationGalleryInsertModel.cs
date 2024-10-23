using Microsoft.AspNetCore.Http;

namespace DataAccessLayer.DTOs.InputModels
{
    public class LocationGalleryInsertModel
    {
        public IFormFile ImageFile { get; set; }  // For image upload
        public bool IsPrimary { get; set; }
        public string ImageCaption { get; set; }
        public int LocationID { get; set; } // Foreign Key
    }
}
