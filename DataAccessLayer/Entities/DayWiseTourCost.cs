using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class DayWiseTourCost
    {
        public int DayWiseTourCostID { get; set; }

        public int PackageID { get; set; }
        public Package? Package { get; set; }

        public decimal OtherCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCost { get; set; }

        // Foreign Key for DayCostCategory
        public int DayCostCategoryID { get; set; }
        public DayCostCategory DayCostCategory { get; set; } // Navigation property
    }
}
