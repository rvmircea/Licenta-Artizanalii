using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artizanalii_Api.DTOs.Producers;
using Artizanalii_Api.Entities.Producers;
using Artizanalii_Api.Repositories.Producers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artizanalii_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerRepository _producerRepository;

        public ProducerController(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducersAsync()
        {
            var producers = from b in await _producerRepository.GetAllProducersAsync()
                select new ProducerDTO
                {
                    Name = b.Name,
                    Description = b.Description,
                    YearFounded = b.YearFounded
                };
            if (producers.ToList().Count == 0)
            {
                return NotFound();
            }
            return Ok(producers);
        }

        [HttpGet("{producerId:int}", Name = "GetProducerAsync")]
        [ActionName("GetProducerAsync")]
        public async Task<ActionResult<Producer>> GetProducerAsync(int producerId)
        {
            var producer = await _producerRepository.GetProducerAsync(producerId);

            if (producer is null)
            {
                return NotFound();
            }
            
            var newProducer = new ProducerDTO
            {
                Name = producer.Name,
                Description = producer.Description,
                YearFounded = producer.YearFounded,
                ProducerAddressId = producer.ProducerAddressId
            };
            
            if (newProducer is null)
            {
                return NotFound();
            }

            return Ok(newProducer);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Producer>> CreateProducerAsync([FromBody] ProducerDTO producerDto)
        {
            if (producerDto is null)
            {
                return BadRequest(ModelState);
            }

            var newProdus = new Producer
            {
                Name = producerDto.Name,
                Description = producerDto.Description,
                YearFounded = producerDto.YearFounded,
                ProducerAddressId = producerDto.ProducerAddressId
            };
            
            
            var createdEntity = await _producerRepository.CreateProducerAsync(newProdus);
            if (createdEntity is null)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(GetProducerAsync), new {producerId = createdEntity.Id}, createdEntity);
        }
    }
}
