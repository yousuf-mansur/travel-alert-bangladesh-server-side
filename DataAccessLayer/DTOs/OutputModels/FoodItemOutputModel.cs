namespace DataAccessLayer.DTOs.OutputModels
{
    public class FoodItemOutputModel
    {
        public int FoodItemID { get; set; }
        public string ItemName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
