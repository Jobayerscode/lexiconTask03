public static class AttackCatalog
{
    public static readonly Attack Ember = new("Ember", ElementType.Fire, 15);
    public static readonly Attack Flamethrower = new("Flamethrower", ElementType.Fire, 25);
    public static readonly Attack WaterGun = new("Water Gun", ElementType.Water, 20);
    public static readonly Attack HydroPump = new("Hydro Pump", ElementType.Water, 30);
    public static readonly Attack VineWhip = new("Vine Whip", ElementType.Grass, 18);
    public static readonly Attack RazorLeaf = new("Razor Leaf", ElementType.Grass, 22);

    public static readonly LegendaryAttack SacredFire = new(Flamethrower);
    public static readonly LegendaryAttack OriginPulse = new(HydroPump);
    public static readonly LegendaryAttack SeedFlare = new(RazorLeaf);
}
