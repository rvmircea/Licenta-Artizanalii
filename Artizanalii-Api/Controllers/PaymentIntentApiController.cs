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
using Artizanalii_Api.DTOs;
using Artizanalii_Api.Repositories.Baskets;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
namespace Artizanalii_Api.Controllers
{
    [Route("create-payment-intent")]
    [ApiController]
    public class PaymentIntentApiController : ControllerBase
    {
        
        [HttpPost]
        // [Authorize(Policy = "buy:products")]
        public async Task<ActionResult> CreateAsync(BasketDto basket)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
            {
                Amount = decimal.ToInt64(basket.TotalPrice * 100),
                Currency = "ron",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true
                },
                ReceiptEmail = "vmircea1771@gmail.com",
            });
            return Ok(new {clientSecret = paymentIntent.ClientSecret});
        }
    }
}
