using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace DataAccessLayer.Entities
{

    public class Payment
    {
        public int PaymentID { get; set; }
        public int BookingID { get; set; }

        // Make PromotionId nullable
        public int? PromotionID { get; set; }

        public int PaymentStatusID { get; set; }
        public int PaymentMethodID { get; set; }
        public string? SubmittedPromo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }
        public string Currency { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string? StripePaymentIntentID { get; set; }
       // public string TransactionID { get; set; }
        public string MobileNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Booking Booking { get; set; }
        public Promotion? Promotion { get; set; } 
       // public Transaction Transactions { get; set; } 
    }

    
}
