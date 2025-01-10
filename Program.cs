using Microsoft.Extensions.Options;
using pokedex.Services;
using pokedex;
using pokedex.Models;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped<IPokemonService, PokemonService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();