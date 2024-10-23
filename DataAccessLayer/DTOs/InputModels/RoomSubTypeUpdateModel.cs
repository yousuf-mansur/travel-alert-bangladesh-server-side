namespace DataAccessLayer.DTOs.InputModels
{
    namespace TravelUpdate.Models.InputModels
    {
        public class RoomSubTypeUpdateModel
        {
            public int RoomSubTypeID { get; set; } // Include ID for updating
            public string SubTypeName { get; set; }
            public int RoomTypeID { get; set; }
        }
    }

}
