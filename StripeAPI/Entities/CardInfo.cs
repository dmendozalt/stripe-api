namespace StripeAPI.Entities
{
    public class CardInfo
    {
        public string Number { get; set; }
        public int ExpMonth { get;  set; }
        public int ExpYear { get; set; }
        public string Cvc { get; set; }
    }
}
