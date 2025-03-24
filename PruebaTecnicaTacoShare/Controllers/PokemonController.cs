using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet]
        public async Task<IActionResult> GetAllPokemons()
        {
            var pokemons = await _context.Pokemons.ToListAsync();
            if (pokemons == null || !pokemons.Any())
                return NotFound(new { message = "No se han encontrado Pokemons" });
            return Ok(pokemons);
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
                Abilities = string.Join(",", pokemon.Abilities.Select(a => a.Ability.Name))
            };
            _context.Pokemons.Add(pokemonEntity);
            await _context.SaveChangesAsync();
            return Ok(pokemon);
            //CreatedAtAction(nameof(GetPokemon), new {name = pokemonEntity.Name});
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, [FromBody] Pokemon updatedPokemon)
        {
            //Primero checar si el pokemon existe
            var pokemonEntity = await _context.Pokemons.FindAsync(id);
            if (pokemonEntity == null)
                return NotFound(new {message = "Pokemon no encontrado"});
            //Actualizamos las propiedades del objeto, o sease del pokemon
            pokemonEntity.Name = updatedPokemon.Name;
            pokemonEntity.Height = updatedPokemon.Height;
            pokemonEntity.Weight = updatedPokemon.Weight;
            pokemonEntity.Abilities = updatedPokemon.Abilities;

            //guardamos los cambios
            await _context.SaveChangesAsync();
            return Ok(pokemonEntity);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            //Verificamos que el pokemon exista
            var pokemonEntity = await _context.Pokemons.FindAsync(id);
            if (pokemonEntity == null)
                return NotFound(new { message = "Pokemon no encontrado" });
            //si se encuentra pues se elimina
            _context.Pokemons.Remove(pokemonEntity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
