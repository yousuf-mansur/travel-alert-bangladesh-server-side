namespace DataAccessLayer.Entities
{
    public class PaymentMethod
    {
        public int PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
