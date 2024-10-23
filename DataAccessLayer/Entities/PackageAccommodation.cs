using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class PackageAccommodation
    {
        public int PackageAccommodationID { get; set; }

        public int PackageID { get; set; } = 1;
        public Package Package { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public int RoomID { get; set; } = 1;
        public Room Room { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal price { get; set; } = 2000;
    }

}