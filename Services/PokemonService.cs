using MongoDB.Driver;
using pokedex.Models;

namespace pokedex.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IMongoCollection<Pokemon> _pokemonCollection;

        public PokemonService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
            _pokemonCollection = database.GetCollection<Pokemon>(configuration["MongoDbSettings:CollectionName"]);
        }

        public async Task<List<Pokemon>> GetAllAsync() =>
            await _pokemonCollection.Find(_ => true).ToListAsync();

        public async Task<Pokemon> GetByIdAsync(string id) =>
            await _pokemonCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Pokemon pokemon)
        {
            if (pokemon == null)
                throw new ArgumentNullException(nameof(pokemon));

            await _pokemonCollection.InsertOneAsync(pokemon);
        }

        public async Task UpdateAsync(string id, Pokemon updatedPokemon)
        {
            if (updatedPokemon == null)
                throw new ArgumentNullException(nameof(updatedPokemon));

            var result = await _pokemonCollection.ReplaceOneAsync(p => p.Id == id, updatedPokemon);

            if (result.MatchedCount == 0)
                throw new KeyNotFoundException($"Pokemon with ID {id} not found.");
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _pokemonCollection.DeleteOneAsync(p => p.Id == id);

            if (result.DeletedCount == 0)
                throw new KeyNotFoundException($"Pokemon with ID {id} not found.");
        }
    }
}
