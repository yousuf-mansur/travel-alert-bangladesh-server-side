using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class PackageFoodItem 
    {
        public int PackageFoodItemID { get; set; }
        public int MealTypeID { get; set; } = 1;
        public int FoodItemID {  get; set; }
        public int PackageID { get; set; }
        public int PackageDayNumber { get; set; } = 1;
        [Column(TypeName = "decimal(18,2)")]
        public decimal FoodQuantity {  get; set; }
        public double FoodUnitPrice { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ItemTotalCost { get; set; } = 800;

        public DateTime ScheduleTime { get; set; }
        public Package? Package { get; set; }        
        public MealType? MealType { get; set; }
        public virtual FoodItem? FoodItem { get; set; }
        
    }
}

