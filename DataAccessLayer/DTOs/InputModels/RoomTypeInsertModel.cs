using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.InputModels
{
    public class RoomTypeInsertModel
    {
        [Required]
        public string TypeName { get; set; }
    }

}
