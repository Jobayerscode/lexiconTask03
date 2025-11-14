public sealed class BattleSystem
{
    private readonly Random _rng = new();

    public record AttackResult(string Attacker, string AttackName, int Raw, double Multiplier, int Damage, bool TargetFainted);

    public AttackResult UseAttack(PokemonInstance attacker, Attack attack, PokemonInstance target)
    {
        int raw = attack.Use(attacker.Level);
        double mult = TypeEffectiveness.Get(attack.Type, target.Species.Type);
        int dmg = (int)(raw * mult);
        target.TakeDamage(dmg);
        return new AttackResult(attacker.Nickname, attack.Name, raw, mult, dmg, target.IsFainted);
    }

    public Attack ChooseRandomAttack(PokemonInstance p) => p.Attacks[_rng.Next(p.Attacks.Count)];
}
