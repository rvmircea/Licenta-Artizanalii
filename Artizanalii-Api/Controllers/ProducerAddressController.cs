using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artizanalii_Api.DTOs.ProducerAddress;
using Artizanalii_Api.Entities.ProducerAddresses;
using Artizanalii_Api.Repositories.ProducerAddresses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artizanalii_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerAddressController : ControllerBase
    {
        private readonly IProducerAddressRepository _producerAddressRepository;

        public ProducerAddressController(IProducerAddressRepository producerAddressRepository)
        {
            _producerAddressRepository = producerAddressRepository;
        }
        
        
        [HttpGet]
        public async Task<ActionResult> GetProducerAddressesAsync()
        {
            var producersAddresses = from pa in await _producerAddressRepository.GetAllProducerAddressAsync()
                select new ProducerAddressDTO
                {
                    Id = pa.Id,
                    City = pa.City,
                    Address = pa.Address,
                    AddressNumber = pa.AddressNumber,
                    ZipCode = pa.ZipCode
                };
            
            if (producersAddresses.ToList().Count == 0)
            {
                return NotFound();
            }
            return Ok(producersAddresses);
        }

        [HttpGet("{producerAddressId:int}", Name = "GetProducerAddressAsync")]
        [ActionName("GetProducerAddressAsync")]
        public async Task<ActionResult<ProducerAddress>> GetProducerAddressAsync(int producerAddressId)
        {
            var producersAddress = await _producerAddressRepository.GetProducerAddressAsync(producerAddressId);
            if (producersAddress is null)
            {
                return NotFound();
            }

            return Ok(producersAddress); 
        }

        [HttpPost("create")]
        public async Task<ActionResult<ProducerAddress>> CreateProducerAddressAsync(
            [FromBody] ProducerAddressDTO producerAddress)
        {
            if (producerAddress is null)
            {
                return BadRequest();
            }

            var newAddress = new ProducerAddress
            {
                Address = producerAddress.Address,
                AddressNumber = producerAddress.AddressNumber,
                City = producerAddress.City,
                ZipCode = producerAddress.ZipCode,
            };

            var createdEntity = await _producerAddressRepository.CreateProducerAddressAsync(newAddress);
            if (createdEntity is null)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(GetProducerAddressAsync), new {createdEntity.Id}, producerAddress);

        }
    }
}