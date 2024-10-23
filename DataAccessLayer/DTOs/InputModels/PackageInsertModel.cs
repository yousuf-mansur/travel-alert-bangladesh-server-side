using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.InputModels
{
    public class PackageInsertModel
    {
        public string PackageTitle { get; set; }
        public string PackageDescription { get; set; }
        public bool IsAvailable { get; set; }
        public int PackageCategoryID { get; set; }
        public int? PackageSubCategoryID { get; set; }
    }

}
