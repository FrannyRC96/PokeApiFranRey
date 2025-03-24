using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTacoShare.Models;
namespace PruebaTecnicaTacoShare.Data
{
    public class PokemonDBContext : DbContext
    {
        public PokemonDBContext(DbContextOptions<PokemonDBContext> options) : base(options) { }
        public DbSet<Pokemon> Pokemons { get; set; }
    }
}
