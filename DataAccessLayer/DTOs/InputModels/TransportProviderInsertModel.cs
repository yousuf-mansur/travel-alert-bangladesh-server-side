using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.InputModels
{
    public class TransportProviderInsertModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string CompanyName { get; set; }

        [Required]
        [StringLength(15)]
        public string ContactNumber { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public bool IsVerified { get; set; } = false; // Default value
    }

}
