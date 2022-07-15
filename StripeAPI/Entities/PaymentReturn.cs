namespace StripeAPI.Entities
{
    public class PaymentReturn
    {
        public string Id { get; set; }
        public long Amount { get; set; } 
        public long AmountReceived { get; set; }
        public string Status { get; set; }
    }
}
