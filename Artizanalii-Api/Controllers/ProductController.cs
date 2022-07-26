using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artizanalii_Api.Entities.Products;
using Artizanalii_Api.Repositories.Products;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artizanalii_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetSingleProductAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        
        [HttpGet("category/{categoryId:int}")]
        public async Task<ActionResult<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
            if (products is null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("page/{page:int}")]
        public async Task<ActionResult<List<Product>>> GetProductsPerPage(int page)
        {
            var products = await _productRepository.GetProductPage(page);
            if (products.Count == 0)
            {
                return NotFound();
            }

            return Ok(products);
        }
    }
}
