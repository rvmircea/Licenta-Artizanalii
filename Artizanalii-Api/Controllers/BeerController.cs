
using Artizanalii_Api.Entities.Beer;
using Artizanalii_Api.Repositories.Beers;
using Microsoft.AspNetCore.Mvc;

namespace Artizanalii_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IBeerRepository _beerRepository;

        public BeersController(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Beer>>> GetAllBeersAsync()
        {
            var beers = await _beerRepository.GetAllBeersAsync();
            if (beers.Count == 0)
            {
                return NotFound();
            }
            return Ok(beers);
        }

        [HttpGet("{beerId}", Name = "GetBeerByIdAsync")]
        public async Task<ActionResult<Beer>> GetBeerByIdAsync(int beerId)
        {
            var beer = await _beerRepository.GetBeerByIdAsync(beerId);

            if(beer is null)
            {
                return NotFound();
            }

            return Ok(beer);

        }

        [HttpPost]
        public async Task<ActionResult<Beer>> PostBeer([FromBody] Beer beer)
        {
            if (beer is null)
            {
                return BadRequest(ModelState);
            }

            var createdEntity =  await _beerRepository.CreateBeerAsync(beer);
            
            if(createdEntity is null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetBeerByIdAsync", new { beerId = beer.Id }, beer);

        }

        [HttpPut]
        public async Task<ActionResult<Beer>> UpdateBeerAsync(int beerId, Beer bereToUpdate)
        {
            var beer = await _beerRepository.GetBeerByIdAsync(beerId);
            if(beer is null)
            {
                return NotFound();
            }

            await _beerRepository.UpdateBeerAsync(beerId, bereToUpdate);
            return Ok(bereToUpdate);
        }

        [HttpDelete("{beerId}")]
        public async Task<ActionResult<Beer>> DeleteBeer(int beerId)
        {
            var beerToDelete = await  _beerRepository.GetBeerByIdAsync(beerId);

            if(beerToDelete is null)
            {
                return NotFound();
            }

            var beerRemoved = await _beerRepository.DeleteBeerAsync(beerId);
            
            if(beerRemoved is null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
