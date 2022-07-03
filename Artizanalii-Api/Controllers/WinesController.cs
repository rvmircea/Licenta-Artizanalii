using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artizanalii_Api.DTOs.Wines;
using Artizanalii_Api.Entities.Wines;
using Artizanalii_Api.Repositories.Wines;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artizanalii_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinesController : ControllerBase
    {
        private readonly IWineRepository _wineRepository;

        public WinesController(IWineRepository wineRepository)
        {
            _wineRepository = wineRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllWinesAsync()
        {
            var wines = await _wineRepository.GetAllWinesAsync();
            if (wines is null)
            {
                return NotFound();
            }

            return Ok(wines);
        }

        [HttpGet("{wineId:int}")]
        [ActionName("GetWineById")]
        public async Task<ActionResult<Wine>> GetWineById(int wineId)
        {
            var wine = await _wineRepository.GetWineByIdAsync(wineId);
            if (wine is null)
            {
                return NotFound();
            }

            return wine;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Wine>> CreateWineById([FromBody] WineDto wineDto)
        {
            var wineToCreate = new Wine()
            {
                Id = wineDto.Id,
                Name = wineDto.Name,
                Description = wineDto.Description,
                Abv = wineDto.Abv,
                Price = wineDto.Price,
                ProducerId = wineDto.ProducerId,
                YearCreated = wineDto.YearCreated
            };

            var wineCreated = await _wineRepository.CreateWineAsync(wineToCreate);
            if (wineCreated is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetWineById), new {wineId = wineCreated.Id}, wineCreated);
        }

        [HttpPut("{wineId:int}")]
        public async Task<ActionResult<Wine>> UpdateWineAsync(int wineId, [FromBody] WineDto wineToUpdate)
        {
            if (wineId != wineToUpdate.Id)
            {
                return BadRequest();
            }
            var updatedWine = new Wine()
            {
                Id = wineToUpdate.Id,
                Name = wineToUpdate.Name,
                Description = wineToUpdate.Description,
                Abv = wineToUpdate.Abv,
                Price = wineToUpdate.Price,
                YearCreated = wineToUpdate.YearCreated,
                ProducerId = wineToUpdate.ProducerId
            };

            var createdWine = await _wineRepository.UpdateWineAsync(wineId, updatedWine);
            if (createdWine is null)
            {
                return BadRequest();
            }
            
            return Ok(createdWine);
        }

        [HttpDelete("{wineId:int}")]
        public async Task<ActionResult> DeleteWineAsync(int wineId)
        {
            var wineToDelete = await _wineRepository.DeleteWineAsync(wineId);
            if (wineToDelete is null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
