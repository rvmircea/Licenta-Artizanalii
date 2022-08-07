using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artizanalii_Api.Entities.Baskets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Text;
using System.Text.Json;
using Artizanalii_Api.Repositories.Baskets;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
namespace Artizanalii_Api.Controllers
{
    [Route("create-payment-intent")]
    [ApiController]
    public class PaymentIntentApiController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public PaymentIntentApiController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        [HttpPost]
        [Authorize(Policy = "buy:products")]
        public async Task<ActionResult> CreateAsync(Basket basket)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = decimal.ToInt64(basket.TotalPrice * 100),
                Currency = "ron",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true
                },
                ReceiptEmail = "vmircea1771@gmail.com",
            });
            await _basketRepository.RemoveAllFromBasketAsync(basket.UserId);
            return Ok(new {clientSecret = paymentIntent.ClientSecret});
        }
    }
}
