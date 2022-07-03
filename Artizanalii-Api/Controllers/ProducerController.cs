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
            var producers = from prod in await _producerRepository.GetAllProducersAsync()
                select new ProducerDTO
                {   
                    Id = prod.Id,
                    Name = prod.Name,
                    Description = prod.Description,
                    YearFounded = prod.YearFounded,
                    ProducerAddressId = prod.ProducerAddressId
                };
            if (producers.ToList().Count == 0)
            {
                return NotFound();
            }
            return Ok(producers);
            
            /*var producers = await _producerRepository.GetAllProducersAsync();
            if (producers is null)
            {
                return NotFound();
            }

            return Ok(producers);*/
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
            
            /*var newProducer = new ProducerDTO
            {   
                Id = producer.Id,
                Name = producer.Name,
                Description = producer.Description,
                YearFounded = producer.YearFounded,
                ProducerAddressId = producer.ProducerAddressId
            };*/

            /*return Ok(newProducer);*/
            return Ok(producer);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Producer>> CreateProducerAsync([FromBody] ProducerDTO producerDto)
        {
            if (producerDto is null)
            {
                return BadRequest(ModelState);
            }

            var newProducer = NewProducer(producerDto);
            
            
            var createdEntity = await _producerRepository.CreateProducerAsync(newProducer);
            if (createdEntity is null)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(GetProducerAsync), new {producerId = createdEntity.Id}, createdEntity);
        }

        [HttpPut("{producerId:int}")]
        public async Task<ActionResult<Producer>> UpdateProducerAsync(int producerId, [FromBody] ProducerDTO producerDto)
        {
            if (producerId != producerDto.Id)
            {
                return BadRequest();
            }
            
            var producerToUpdate = await _producerRepository.GetProducerAsync(producerDto.Id);
            if (producerToUpdate is null)
            {
                return NotFound();
            }

            var updatedProducer = NewProducer(producerDto);

            var newProducer = await _producerRepository.UpdateProducerAsync(producerId ,updatedProducer);
            if (newProducer is null)
            {
                return BadRequest();
            }
            
            return Ok(newProducer);
        }
        
        [HttpDelete("{producerId}")]
        public async Task<ActionResult<Producer>> DeleteProducerAsync(int producerId)
        {
            var producerToDelete = await _producerRepository.DeleteProducerAsync(producerId);
            if (producerToDelete is null)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static Producer? NewProducer(ProducerDTO producerDto)
        {
            return new Producer
            {
                Id = producerDto.Id,
                Name = producerDto.Name,
                Description = producerDto.Description,
                YearFounded = producerDto.YearFounded,
                ProducerAddressId = producerDto.ProducerAddressId
            };
        }
    }
}
