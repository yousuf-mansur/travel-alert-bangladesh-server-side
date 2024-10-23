using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class PackageTransportation
    {
        public int PackageTransportationID { get; set; }

        public int PackageID { get; set; }

        public int TransportationTypeID { get; set; }

        public int TransportationCatagoryID { get; set; }

        public int TransportationID { get; set; }

        public int SeatBooked { get; set; } = 10; // Default value for SeatBooked

        [StringLength(500)]
        public string PackageTransportationDescription { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PerHeadTransportCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ItemTransportTotalCost { get; set; }

        // Navigation properties
        public virtual Package Package { get; set; } 
        public virtual Transportation Transportation { get; set; } 
        public virtual TransportationType TransportationType { get; set; } 
        public virtual TransportationCatagory TransportationCatagory { get; set; } 
        public virtual ICollection<Seats> Seats { get; set; } = new List<Seats>(); 
    }

}