public sealed class PokemonSpecies
{
    public string Name { get; }
    public ElementType Type { get; }
    public int BaseMaxHP { get; }
    public EvolutionRule Evolution { get; } 
    public IReadOnlyList<Attack> DefaultAttacks { get; }

    public PokemonSpecies(string name, ElementType type, int baseMaxHp, EvolutionRule evolution, IEnumerable<Attack> defaults)
    {
        Name = name;
        Type = type;
        BaseMaxHP = baseMaxHp;
        Evolution = evolution;
        DefaultAttacks = defaults.ToList().AsReadOnly();
    }
}
