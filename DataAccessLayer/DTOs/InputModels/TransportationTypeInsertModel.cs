using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.InputModels
{
    public class TransportationTypeInsertModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "TypeName cannot be longer than 50 characters.")]
        public string TypeName { get; set; }
    }

}
