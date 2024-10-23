using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.InputModels
{
    public class RoomTypeUpdateModel
    {
        public int RoomTypeID { get; set; } // Include RoomTypeID for updates

        [Required]
        public string TypeName { get; set; }
    }

}
