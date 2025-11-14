
public sealed class EvolutionRule
{
    public int EvolveAtLevel { get; }
    public Func<PokemonSpecies> NextSpecies { get; }
    public EvolutionRule(int evolveAtLevel, Func<PokemonSpecies> nextSpecies)
    {
        EvolveAtLevel = evolveAtLevel;
        NextSpecies = nextSpecies;
    }

    public bool CanEvolve(int level) => level >= EvolveAtLevel;
}
