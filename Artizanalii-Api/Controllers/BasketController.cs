using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artizanalii_Api.Entities.BasketItems;
using Artizanalii_Api.Entities.Baskets;
using Artizanalii_Api.Repositories.Baskets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artizanalii_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Basket>> GetUserBasketAsync(string userId)
        {
            var basket = await _basketRepository.GetBasket(userId);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddToCartAsync(string userId, BasketItem item)
        {
            var result = await _basketRepository.AddToBasket(userId, item);
            if (result)
            {
                return Ok("Added");
            }

            return BadRequest();
        }
    }
}
