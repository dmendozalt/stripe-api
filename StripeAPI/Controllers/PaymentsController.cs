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
        [HttpPost]
        public string Get([FromBody] CardInfo cardInfo)
        {
            StripeConfiguration.ApiKey = "sk_test_51LLgOpHNoFBenqbdMnR7j1MfKUPJ9e1mnU6TQlSPHsw78Jj78zNhblpkBR1cmlkBnUN3egLwwcvVxBquTXA4AKOI00HTBT5Tmp";
            return PlacePayment(cardInfo).Id;
        }


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
                    Cvc = card.Cvc,
                },
            };
            var service = new PaymentMethodService();
            return service.Create(options);
        }

        private PaymentIntent PlacePayment(CardInfo cardInfo)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = 200,
                Currency = "usd",
                PaymentMethodTypes = new List<string>
                  {
                    "card",
                  },
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
