namespace StripeAPI.Entities
{
    public class PaymentReturn: IPaymentIntent
    {
        public long Amount { get; set; } 
        public long AmountReceived { get; set; }
        public string Status { get; set; }
    }
}
