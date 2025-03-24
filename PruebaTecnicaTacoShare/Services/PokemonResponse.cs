namespace PruebaTecnicaTacoShare.Services
{
    public class PokemonResponse
    {
        public string Name {  get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<AbilityInfo> Abilities { get; set; }
    }
    public class AbilityInfo
    {
        public Ability Ability { get; set; }
    }
    public class Ability
    {
        public string Name { get; set; }
    }
}
