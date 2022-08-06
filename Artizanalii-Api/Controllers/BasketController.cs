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
            var basket = await _basketRepository.GetBasketAsync(userId);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddToCartAsync(string userId, BasketItem item)
        {
            var result = await _basketRepository.AddToBasketAsync(userId, item);
            if (result)
            {
                return Ok("Added");
            }

            return BadRequest();
        }

        [HttpDelete("{basketItemId:int}")]
        public async Task<ActionResult<bool>> DeleteBasketItemAsync(int basketItemId)
        {
            var result = await _basketRepository.RemoveFromBasketAsync(basketItemId);
            if (result is false)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteAllBasketItemsAsync(string userId)
        {
            var result = await _basketRepository.RemoveAllFromBasketAsync(userId);
            if (result is false)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
