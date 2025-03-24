using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PruebaTecnicaTacoShare.Data;
using PruebaTecnicaTacoShare.Models;
using PruebaTecnicaTacoShare.Services;
using System.Threading.Tasks;

namespace PruebaTecnicaTacoShare.Controllers
{
    [Route("api/pokemon")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly PokeApiService _pokeApiService;
        private readonly PokemonDBContext _context;
        public PokemonController(PokeApiService pokeapiservice, PokemonDBContext context)
        {
            _pokeApiService = pokeapiservice;
            _context = context;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPokemon(string name)
        {
            var pokemon = await _pokeApiService.GetPokemonAsync(name);
            if (pokemon == null)
                return NotFound(new { message = "Pokemon no encontrado" });
            return Ok(pokemon);
        }
        [HttpPost("{name}")]
        public async Task<IActionResult> SavePokemon(string name)
        {
            //Esto guarda en la DB
            var pokemon = await _pokeApiService.GetPokemonAsync(name);
            if (pokemon == null)
                return NotFound(new { message = "Pokemon no encontrado" });
            var pokemonEntity = new Pokemon
            {
                Name = pokemon.Name,
                Height = pokemon.Height,
                Weight = pokemon.Weight,
                Abilities = pokemon.Abilities.Select(a => a.Ability.Name).ToList()
            };
            _context.Pokemons.Add(pokemonEntity);
            await _context.SaveChangesAsync();
            return Ok(pokemon);
                //CreatedAtAction(nameof(GetPokemon), new {name = pokemonEntity.Name});
        }
    }
}
