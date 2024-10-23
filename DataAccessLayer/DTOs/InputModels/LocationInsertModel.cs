namespace DataAccessLayer.DTOs.InputModels
{
    public class LocationInsertModel
    {
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public int StateID { get; set; } // Foreign Key
    }
}
