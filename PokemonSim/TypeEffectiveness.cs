public static class TypeEffectiveness
{
    public static double Get(ElementType atk, ElementType def)
    {
        if (atk == ElementType.Fire && def == ElementType.Grass) return 2.0;
        if (atk == ElementType.Fire && def == ElementType.Water) return 0.5;
        if (atk == ElementType.Water && def == ElementType.Fire) return 2.0;
        if (atk == ElementType.Water && def == ElementType.Grass) return 0.5;
        if (atk == ElementType.Grass && def == ElementType.Water) return 2.0;
        if (atk == ElementType.Grass && def == ElementType.Fire) return 0.5;
        return 1.0;
    }
}
