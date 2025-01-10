using pokedex.Models;

namespace pokedex.Services
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> GetAllAsync();
        Task<Pokemon> GetByIdAsync(string id);
        Task CreateAsync(Pokemon pokemon);
        Task UpdateAsync(string id, Pokemon updatedPokemon);
        Task DeleteAsync(string id);
    }
}