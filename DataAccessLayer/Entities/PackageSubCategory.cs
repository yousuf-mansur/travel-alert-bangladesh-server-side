namespace DataAccessLayer.Entities
{
    public class PackageSubCategory
    {
        public int PackageSubCategoryID { get; set; }
        public int PackageCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual PackageCategory PackageCategory { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
    }

}