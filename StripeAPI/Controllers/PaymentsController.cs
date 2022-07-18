using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using StripeAPI.Entities;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StripeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        public PaymentsController()
        {
            StripeConfiguration.ApiKey = "sk_test_51LLgOpHNoFBenqbdMnR7j1MfKUPJ9e1mnU6TQlSPHsw78Jj78zNhblpkBR1cmlkBnUN3egLwwcvVxBquTXA4AKOI00HTBT5Tmp";
        }

        [EnableCors("CorsPolicy")]
        [HttpPost]
        public IPaymentIntent Pay([FromBody] CardInfo cardInfo)
        {
            return new() { Id = PlacePayment(cardInfo).Id };
        }

        [EnableCors("CorsPolicy")]
        [HttpGet("{id}")]
        public PaymentReturn GetPaymentIntent(string id)
        {
            var paymentIntent = RetrievePayment(id);
            return new() { 
                Id=paymentIntent.Id,
                Amount=paymentIntent.Amount,
                AmountReceived=paymentIntent.AmountReceived,
                Status=paymentIntent.Status,
            };
        }

        private PaymentMethod CreatePaymentMethod(CardInfo card)
        {
            var options = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Number = card.Number,
                    ExpMonth = card.ExpMonth,
                    ExpYear = card.ExpYear,
                    Cvc = card.Cvv,
                },
            };
            var service = new PaymentMethodService();
            return service.Create(options);
        }

        private PaymentIntent PlacePayment(CardInfo cardInfo)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = 2000,
                Currency = "usd",
                PaymentMethod= CreatePaymentMethod(cardInfo).Id,
                ReceiptEmail="dolceymendozajr@gmail.com",
                Confirm=true
            };
            var service = new PaymentIntentService();
            return service.Create(options);
        }

        private PaymentIntent RetrievePayment(string id)
        {
            var service =new PaymentIntentService();
            return service.Get(id);
        }

    }
}
