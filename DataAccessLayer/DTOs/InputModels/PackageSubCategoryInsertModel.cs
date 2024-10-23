namespace DataAccessLayer.DTOs.InputModels
{
    public class PackageSubCategoryInsertModel
    {
        public int PackageCategoryID { get; set; } 
        public string SubCategoryName { get; set; } = ""; 
        public string Description { get; set; } = ""; 

    }

}
