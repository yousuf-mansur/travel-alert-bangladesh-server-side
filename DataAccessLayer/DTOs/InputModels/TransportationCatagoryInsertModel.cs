using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.InputModels
{
    public class TransportationCatagoryInsertModel
    {
        [Required]
        [StringLength(100)]
        public string TransportationCatagoryName { get; set; }
    }

}
