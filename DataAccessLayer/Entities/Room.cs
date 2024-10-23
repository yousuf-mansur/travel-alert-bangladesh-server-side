using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Room
    {
        public int RoomID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AveragePrice { get; set; }

        public int MaxOccupancy { get; set; }
        public bool IsAvailable { get; set; }
        // Foreign Key
        public int HotelID { get; set; }
        public Hotel Hotel { get; set; }

        public int RoomTypeID { get; set; } = 1;
        public RoomType RoomType { get; set; }

        public int RoomSubTypeID { get; set; } = 1;
        public RoomSubType RoomSubType { get; set; }

        public ICollection<PackageAccommodation> PackageAccommodations { get; set; }
    }

}