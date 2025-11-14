public class Attack
{
    public string Name { get; }
    public ElementType Type { get; }
    public int BasePower { get; }

    public Attack(string name, ElementType type, int basePower)
    {
        Name = name;
        Type = type;
        BasePower = basePower;
    }

    public virtual int Use(int level) => BasePower + level;
}

public class LegendaryAttack : Attack
{
    public LegendaryAttack(Attack baseAttack)
        : base($"Legendary {baseAttack.Name}", baseAttack.Type, baseAttack.BasePower) {}

    public override int Use(int level) => BasePower + (level * 2);
}
