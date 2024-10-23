using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.InputModels
{
    public class PackageTransportationInsertModel
    {
        //public int PackageID { get; set; }

        public int TransportationTypeID { get; set; }

        public int TransportationCatagoryID { get; set; }

        public int TransportationID { get; set; }

        public int SeatBooked { get; set; } = 10; 


        [StringLength(1000)]
        public string PackageTransportationDescription { get; set; } = string.Empty; 

        [Column(TypeName = "decimal(18,2)")]
        public decimal PerHeadTransportCost { get; set; }
    }


}
