public class PokemonInstance
{
    public PokemonSpecies Species { get; private set; }
    public string Nickname { get; private set; }
    public int Level { get; private set; }
    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }
    private readonly List<Attack> _attacks;
    public IReadOnlyList<Attack> Attacks => _attacks.AsReadOnly();
    public bool IsFainted => CurrentHP <= 0;

    public PokemonInstance(PokemonSpecies species, int level, IEnumerable<Attack> attacks = null, string nickname = null)
    {
        Species = species ?? throw new ArgumentNullException(nameof(species));
        Level = Math.Max(1, level);
        Nickname = string.IsNullOrWhiteSpace(nickname) ? species.Name : nickname;
        MaxHP = species.BaseMaxHP;
        CurrentHP = MaxHP;
        _attacks = (attacks?.ToList() ?? species.DefaultAttacks.ToList());
    }

    public string Speak() => Species.Name switch
    {
        "Charmander" => "Char char!",
        "Charmeleon" => "Charmeleon!",
        "Charizard" => "CHARIZARD!",
        "Squirtle" => "Squirtle squirt!",
        "Wartortle" => "Wartortle!",
        "Blastoise" => "BLASTOISE!",
        "Bulbasaur" => "Bulba bulba!",
        "Ivysaur" => "Ivysaur!",
        "Venusaur" => "VENUSAUR!",
        _ => $"{Nickname}!"
    };

    public void HealPercent(double percent)
    {
        int heal = (int)(MaxHP * percent);
        CurrentHP = Math.Min(MaxHP, CurrentHP + heal);
    }

    public void TakeDamage(int damage)
    {
        CurrentHP = Math.Max(0, CurrentHP - Math.Max(0, damage));
    }

    public void LevelUp()
    {
        Level++;
        if (Species.Evolution?.CanEvolve(Level) == true)
        {
            Species = Species.Evolution.NextSpecies();
            // Change MaxHP when evolving
            MaxHP = Species.BaseMaxHP;
            CurrentHP = MaxHP;
        }
    }
}
