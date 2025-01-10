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
        public async Task<ActionResult<List<Pokemon>>> GetAll() =>
            await _pokemonService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> GetById(string id)
        {
            var pokemon = await _pokemonService.GetByIdAsync(id);

            if (pokemon == null)
                return NotFound();

            return pokemon;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pokemon pokemon)
        {
            await _pokemonService.CreateAsync(pokemon);
            return CreatedAtAction(nameof(GetById), new { id = pokemon.Id }, pokemon);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Pokemon updatedPokemon)
        {
            var existingPokemon = await _pokemonService.GetByIdAsync(id);

            if (existingPokemon == null)
                return NotFound();

            await _pokemonService.UpdateAsync(id, updatedPokemon);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var pokemon = await _pokemonService.GetByIdAsync(id);

            if (pokemon == null)
                return NotFound();

            await _pokemonService.DeleteAsync(id);

            return NoContent();
        }
    }
}
