using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PruebaTecnicaTacoShare.Services
{
    public class PokeApiService
    {
        private readonly HttpClient _httpClient;

        public PokeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }
        public async Task<PokemonResponse?> GetPokemonAsync(string name)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}");
            if (!response.IsSuccessStatusCode)
                return null;
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PokemonResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
