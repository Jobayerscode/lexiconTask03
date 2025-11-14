public static class SpeciesRepository
{
    public static PokemonSpecies Charmander() =>
        new("Charmander", ElementType.Fire, 100,
            new EvolutionRule(16, Charmeleon),
            new[] { AttackCatalog.Ember, AttackCatalog.Flamethrower });

    public static PokemonSpecies Charmeleon() =>
        new("Charmeleon", ElementType.Fire, 120,
            new EvolutionRule(36, Charizard),
            new[] { AttackCatalog.Flamethrower, AttackCatalog.SacredFire });

    public static PokemonSpecies Charizard() =>
        new("Charizard", ElementType.Fire, 150,
            null,
            new[] { AttackCatalog.Flamethrower, AttackCatalog.SacredFire });

    public static PokemonSpecies Squirtle() =>
        new("Squirtle", ElementType.Water, 100,
            new EvolutionRule(16, Wartortle),
            new[] { AttackCatalog.WaterGun, AttackCatalog.HydroPump });

    public static PokemonSpecies Wartortle() =>
        new("Wartortle", ElementType.Water, 120,
            new EvolutionRule(36, Blastoise),
            new[] { AttackCatalog.HydroPump, AttackCatalog.OriginPulse });

    public static PokemonSpecies Blastoise() =>
        new("Blastoise", ElementType.Water, 150,
            null,
            new[] { AttackCatalog.HydroPump, AttackCatalog.OriginPulse });

    public static PokemonSpecies Bulbasaur() =>
        new("Bulbasaur", ElementType.Grass, 100,
            new EvolutionRule(16, Ivysaur),
            new[] { AttackCatalog.VineWhip, AttackCatalog.RazorLeaf });

    public static PokemonSpecies Ivysaur() =>
        new("Ivysaur", ElementType.Grass, 120,
            new EvolutionRule(32, Venusaur),
            new[] { AttackCatalog.RazorLeaf, AttackCatalog.SeedFlare });

    public static PokemonSpecies Venusaur() =>
        new("Venusaur", ElementType.Grass, 150,
            null,
            new[] { AttackCatalog.RazorLeaf, AttackCatalog.SeedFlare });
}
