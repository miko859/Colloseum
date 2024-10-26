public static class BuffDebuffData
{
    public static BuffDebuffConfig BurningConfig = new BuffDebuffConfig
    {
        Id = 1,
        Name = "Burning",
        DamagePerTick = 2,
        Frequency = 1f,
        Duration = 4.5f
    };

    public static BuffDebuffConfig FreezingConfig = new BuffDebuffConfig
    {
        Id = 2,
        Name = "Freezing",
        MovementSpeedChangedBy = 2.5f,
        Duration = 3f
    };

    public static BuffDebuffConfig PoisonConfig = new BuffDebuffConfig
    {
        Id = 3,
        Name = "Posion",
        Duration = 5f,
        DamagePerTick = 2
    };

    public static BuffDebuffConfig BleedingConfig = new BuffDebuffConfig
    {
        Id = 4,
        Name = "Bleeding",
        Duration = 6f,
        DamagePerTick = 1
    };

    public static BuffDebuffConfig WeaknessConfig = new BuffDebuffConfig
    {
        Id = 5,
        Name = "Weakness",
        Duration = 4f,
        DamageChangedBy = -3
    };
}