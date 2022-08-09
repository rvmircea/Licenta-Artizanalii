using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artizanalii_Api.Entities.Baskets;
using Artizanalii_Api.Entities.Orders;
using Artizanalii_Api.Repositories.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artizanalii_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrderAsync(string userId)
        {
            var orders = await _orderRepository.GetAllOrdersFromUserAsync(userId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrderAsync(string userId, Basket basket)
        {
            var newBasket = await _orderRepository.CreateOrderAsync(userId, basket);
            if (newBasket is null)
            {
                return BadRequest();
            }

            return Ok(newBasket);
        }

    }
}
