namespace DataAccessLayer.DTOs.InputModels
{
    public class PackageFacilityInsertModel
    {
        public int PackageID { get; set; }
        public List<int> FacilityIDs { get; set; } 
    }

}
