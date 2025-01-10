using MongoDB.Driver;
using pokedex.Models;
using Microsoft.Extensions.Options;

namespace pokedex.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IMongoCollection<Pokemon> _pokemonCollection;

        public PokemonService(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _pokemonCollection = database.GetCollection<Pokemon>(settings.Value.CollectionName);
        }

        public async Task<List<Pokemon>> GetAllAsync() =>
            await _pokemonCollection.Find(_ => true).ToListAsync();

        public async Task<Pokemon> GetByIdAsync(string id) =>
            await _pokemonCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Pokemon pokemon) =>
            await _pokemonCollection.InsertOneAsync(pokemon);

        public async Task UpdateAsync(string id, Pokemon updatedPokemon) =>
            await _pokemonCollection.ReplaceOneAsync(p => p.Id == id, updatedPokemon);

        public async Task DeleteAsync(string id) =>
            await _pokemonCollection.DeleteOneAsync(p => p.Id == id);
    }
}
