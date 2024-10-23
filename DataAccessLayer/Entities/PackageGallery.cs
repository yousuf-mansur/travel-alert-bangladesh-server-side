namespace DataAccessLayer.Entities
{
    public class PackageGallery
    {
        public int PackageGalleryID { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
        public string ImageCaption { get; set; }

        // Foreign Key
        public int PackageID { get; set; }
        public Package Package { get; set; }
    }

}
