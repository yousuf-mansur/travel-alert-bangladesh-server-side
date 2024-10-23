using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DTOs.InputModels
{
    public class PackageFoodItemInsertModel
    {
        public int MealTypeID { get; set; } = 1;
        public int FoodItemID { get; set; }
        //public int PackageID { get; set; }
        public int PackageDayNumber { get; set; } = 1; 
        [Column(TypeName = "decimal(18,2)")]
        public decimal FoodQuantity { get; set; }
        public double FoodUnitPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ItemTotalCost { get; set; } = 800; 
        public DateTime ScheduleTime { get; set; }
    }

}
