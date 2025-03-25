using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTacoShare.Data;
using PruebaTecnicaTacoShare.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaTecnicaTacoShare.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonDBContext _context;

        public PokemonRepository(PokemonDBContext context)
        {
            _context = context;
        }
        public async Task<Pokemon> GetPokemonByidAsync(int id)
        {
            return await _context.Pokemons.FindAsync(id);
        }
        public async Task<Pokemon> GetPokemonBynameAsync(string name)
        {
            return await _context.Pokemons.FirstOrDefaultAsync(p => p.Name == name);
        }
        public async Task<IEnumerable<Pokemon>> GetAllPokemonAsync()
        {
            return await _context.Pokemons.ToListAsync();
        }
        public async Task AddPokemonAsync(Pokemon pokemon)
        {
            await _context.Pokemons.AddAsync(pokemon);
        }
        public async Task UpdatePokemonAsync(Pokemon pokemon)
        {
            _context.Pokemons.Update(pokemon);
        }
        public async Task RemovePokemonAsync(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
                _context.Pokemons.Remove(pokemon);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }     
    }
}
