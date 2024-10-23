namespace DataAccessLayer.Entities
{
    public class PackageCategory
    {
        public int PackageCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<PackageSubCategory> PackageSubCategories { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
    }

}