using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaTacoShare.Models;
using PruebaTecnicaTacoShare.Repositories;
using PruebaTecnicaTacoShare.Services;
using System.Threading.Tasks;

namespace PruebaTecnicaTacoShare.Controllers
{
    [Route("api/pokemon")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly PokeApiService _pokeApiService;
        public PokemonController(IPokemonRepository pokemonRepository, PokeApiService pokeapiservice)
        {
            _pokemonRepository = pokemonRepository;
            _pokeApiService = pokeapiservice;
        }
        
        [HttpGet("{name}")]
        public async Task<IActionResult> GetPokemon(string name)
        {
            var pokemon = await _pokemonRepository.GetPokemonBynameAsync(name);
            if (pokemon != null)
                return Ok(pokemon);
            var externalPokemon = await _pokeApiService.GetPokemonAsync(name);
            if (externalPokemon == null)
                return NotFound(new { message = "Pokemon no encontrado ni en la API ni en la DB" });
            var apiPokemon = new
            {
                Name = externalPokemon.Name,
                Height = externalPokemon.Height,
                Weight = externalPokemon.Weight,
                Abilities = string.Join(",", externalPokemon.Abilities.Select(a => a.Ability.Name))
            };

            return Ok(apiPokemon);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllPokemons()
        {
            var pokemons = await _pokemonRepository.GetAllPokemonAsync();
            return Ok(pokemons);
        }
        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetPokemon(int id)
        {
            var pokemon = await _pokemonRepository.GetPokemonByidAsync(id);
            if(pokemon == null)
                return NotFound(new {message = "Pokemon no encontrado" });
            return Ok(pokemon);
        }

        [HttpPost("{name}")]
        //[FromBody] Pokemon pokemon, 
        public async Task<IActionResult> AddPokemon(string name)
        {
            var pokemon = await _pokeApiService.GetPokemonAsync(name);
            if(pokemon == null)
                return NotFound(new { message = "Pokemon no encontrado" });
            var existingPokemon = await _pokemonRepository.GetPokemonBynameAsync(pokemon.Name);
            if (existingPokemon != null)
                return Conflict(new { message = "Este Pokemon ya existe en la base de datos" });
            var newPokemon = new Pokemon
            {
                Name = pokemon.Name,
                Height = pokemon.Height,
                Weight = pokemon.Weight,
                Abilities = string.Join(",", pokemon.Abilities.Select(a => a.Ability.Name))
            };
            await _pokemonRepository.AddPokemonAsync(newPokemon);
            await _pokemonRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPokemon), new { id = newPokemon.id }, newPokemon);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, [FromBody] Pokemon updatedPokemon)
        {
            if (updatedPokemon == null)
                return BadRequest(new { message = "Pokemon no encontrado" });
            var existingPokemon = await _pokemonRepository.GetPokemonByidAsync(id);
            if(existingPokemon == null)
                return BadRequest(new { message = "Pokemon no encontrado" });
            existingPokemon.Name = updatedPokemon.Name;
            existingPokemon.Height = updatedPokemon.Height;
            existingPokemon.Weight = updatedPokemon.Weight;
            existingPokemon.Abilities = updatedPokemon.Abilities;

            await _pokemonRepository.UpdatePokemonAsync(existingPokemon);
            await _pokemonRepository.SaveChangesAsync();

            return Ok(existingPokemon);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeletePokemon(int id)
        {
            var pokemon = await _pokemonRepository.GetPokemonByidAsync(id);
            if(pokemon == null)
                return NotFound(new {message = "Pokemon no encontrado"});
            await _pokemonRepository.RemovePokemonAsync(id);
            await _pokemonRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
