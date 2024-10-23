namespace DataAccessLayer.DTOs.InputModels
{
    public class FoodItemInputModel
    {
        public int FoodItemID { get; set; }
        public string ItemName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
