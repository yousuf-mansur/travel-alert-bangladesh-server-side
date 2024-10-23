    namespace DataAccessLayer.DTOs.InputModels
    {
    public class PackageLocationInsertModel
    {
        public int PackageID { get; set; }   // Foreign Key for Package
        public int LocationID { get; set; }   // Foreign Key for Location
    }
}
