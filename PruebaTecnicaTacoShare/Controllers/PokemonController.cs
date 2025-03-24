using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaTacoShare.Services;
using System.Threading.Tasks;

namespace PruebaTecnicaTacoShare.Controllers
{
    [Route("api/pokemon")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly PokeApiService _pokeApiService;
        public PokemonController(PokeApiService pokeapiservice)
        {
            _pokeApiService = pokeapiservice;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPokemon(string name)
        {
            var pokemon = await _pokeApiService.GetPokemonAsync(name);
            if (pokemon == null)
                return NotFound(new { message = "Pokemon no encontrado" });
            return Ok(pokemon);
        }
    }
}
