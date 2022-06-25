using Artizanalii_Api.DTOs.Beers;
using Artizanalii_Api.Entities.Beers;
using Artizanalii_Api.Repositories.Beers;
using Artizanalii_Api.Repositories.Producers;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace Artizanalii_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IProducerRepository _producerRepository;

        public BeersController(IBeerRepository beerRepository, IProducerRepository producerRepository)
        {
            _beerRepository = beerRepository;
            _producerRepository = producerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<BeerDTO>> GetAllBeersAsync()
        {
            /*var beers = from b in await _beerRepository.GetAllBeersAsync()
                                        select new BeerDTO
                                        {   
                                            Id = b.Id,
                                            Name = b.Name,
                                            Description = b.Description,
                                            BeerType = b.BeerType,
                                            Price = b.Price,
                                            Abv = b.Abv,
                                            ProducerId = b.ProducerId
                                        };
            
            if (beers.ToList().Count == 0)
            {
                return NotFound();
            }
            return Ok(beers);*/

            var beers = await _beerRepository.GetAllBeersAsync();
            if (beers is null)
            {
                return NotFound();
            }

            return Ok(beers);

        }
        
        [HttpGet("{beerId:int}", Name = "GetBeerAsync")]
        [ActionName("GetBeerAsync")]
        public async Task<ActionResult<Beer>> GetBeerAsync(int beerId)
        {
            var beer = await _beerRepository.GetBeerByIdAsync(beerId);
            if (beer is null)
            {
                return NotFound();
            }
            /*var newBeer = new BeerDTO
            {   
                Id = beer.Id,
                Name = beer.Name,
                Description = beer.Description,
                BeerType = beer.BeerType,
                Price = beer.Price,
                Abv = beer.Abv,
                ProducerId = beer.ProducerId,
            };

            if(newBeer is null)
            {
                return NotFound();
            }*/

            return Ok(beer);

        }

        [HttpPost("create")]
        public async Task<ActionResult<Beer>> PostBeer([FromBody] BeerDTO beer)
        {
            if (beer is null)
            {
                return BadRequest(ModelState);
            }

            var beerToCreate = new Beer
            {   
                Name = beer.Name,
                Description = beer.Description,
                BeerType = beer.BeerType,
                Price = beer.Price,
                Abv = beer.Abv,
                ProducerId = beer.ProducerId
            };
            
            var createdEntity =  await _beerRepository.CreateBeerAsync(beerToCreate);
            await _producerRepository.UpdateProducerAsync(beerToCreate.Id, await _producerRepository.GetProducerAsync(beerToCreate.ProducerId));
            if (createdEntity == null)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(GetBeerAsync), new { beerId = createdEntity.Id }, beer);
        }

        [HttpPut("{beerId:int}")]
        public async Task<ActionResult<Beer>> UpdateBeerAsync(int beerId, [FromBody] BeerDTO beerToUpdate)
        {
            var beer = await _beerRepository.GetBeerByIdAsync(beerId);
            
            if(beer is null)
            {
                return NotFound();
            }

            var updatedBeer = new Beer
            {
                Id = beerToUpdate.Id,
                Name = beerToUpdate.Name,
                Description = beerToUpdate.Description,
                BeerType = beerToUpdate.BeerType,
                Abv = beerToUpdate.Abv,
                Price = beerToUpdate.Price,
                ProducerId = beerToUpdate.ProducerId
            };
            
            await _beerRepository.UpdateBeerAsync(beerId, updatedBeer);
            await _producerRepository.UpdateProducerAsync(updatedBeer.ProducerId, await _producerRepository.GetProducerAsync(updatedBeer.ProducerId));
            return Ok(beerToUpdate);
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
