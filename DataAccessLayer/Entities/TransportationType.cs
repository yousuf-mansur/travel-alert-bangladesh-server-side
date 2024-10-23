using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class TransportationType
    {
        public int TransportationTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }  // AC, Non AC, etc.

        public ICollection<Transportation> Transportations { get; set; } = new List<Transportation>();
        public ICollection<PackageTransportation> PackageTransportations { get; set; }
    }

}