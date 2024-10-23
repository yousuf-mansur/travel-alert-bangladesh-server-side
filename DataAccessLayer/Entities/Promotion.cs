using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Promotion
    {
        public int PromotionID { get; set; }
        public string PromotionTitle { get; set; }
        public string PromoCode { get; set; }
        public string PromotionType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int PromotionImageID { get; set; }
        public PromotionImage PromotionImage { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
