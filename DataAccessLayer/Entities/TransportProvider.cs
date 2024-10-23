using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class TransportProvider
    {
        public int TransportProviderID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string CompanyName { get; set; }

        [Required]
        [StringLength(15)]
        public string ContactNumber { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public bool IsVerified { get; set; }

        public ICollection<Transportation> Transportations { get; set; }
    }

}
