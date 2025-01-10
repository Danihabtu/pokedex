using Microsoft.AspNetCore.Mvc;
using pokedex.Models;
using pokedex.Services;

namespace pokedex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pokemon>>> GetAll()
        {
            try
            {
                return await _pokemonService.GetAllAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> GetById(string id)
        {
            try
            {
                var pokemon = await _pokemonService.GetByIdAsync(id);

                if (pokemon == null)
                    return NotFound(new { Message = $"Pokemon with ID {id} not found." });

                return pokemon;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pokemon pokemon)
        {
            try
            {
                await _pokemonService.CreateAsync(pokemon);
                return CreatedAtAction(nameof(GetById), new { id = pokemon.Id }, pokemon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Pokemon updatedPokemon)
        {
            try
            {
                await _pokemonService.UpdateAsync(id, updatedPokemon);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _pokemonService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
