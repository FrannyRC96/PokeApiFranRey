using PruebaTecnicaTacoShare.Models;

namespace PruebaTecnicaTacoShare.Repositories
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<Pokemon>> GetAllPokemonAsync();
        Task<Pokemon> GetPokemonByidAsync(int id);
        Task<Pokemon> GetPokemonBynameAsync(string name);
        Task AddPokemonAsync (Pokemon pokemon);
        Task UpdatePokemonAsync (Pokemon pokemon);
        Task RemovePokemonAsync (int id);
        Task SaveChangesAsync();
    }
}
