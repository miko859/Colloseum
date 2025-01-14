public static class BuffDebuffData
{
    public static BuffDebuffConfig BurningConfig = new BuffDebuffConfig
    {
        Id = 1,
        Name = "Burning",
        DamagePerTick = 2,
        Frequency = 1,
        Duration = 4.5f
    };

    public static BuffDebuffConfig FreezingConfig = new BuffDebuffConfig
    {
        Id = 2,
        Name = "Freezing",
        MovementSpeedChangedBy = 0.1f,
        Duration = 3
    };

    public static BuffDebuffConfig PoisonConfig = new BuffDebuffConfig
    {
        Id = 3,
        Name = "Posion",
        Duration = 5f,
        DamagePerTick = 2,
        Frequency = 1
    };

    public static BuffDebuffConfig BleedingConfig = new BuffDebuffConfig
    {
        Id = 4,
        Name = "Bleeding",
        Duration = 6,
        DamagePerTick = 1, 
        Frequency = 1
        
    };

    public static BuffDebuffConfig WeaknessConfig = new BuffDebuffConfig
    {
        Id = 5,
        Name = "Weakness",
        Duration = 4,
        DamageChangedBy = -3
    };

    public static BuffDebuffConfig SilenceConfig = new BuffDebuffConfig
    {
        Id = 6,
        Name = "Silence",
        Duration = 8,
        DisableSpellsSkills = true,
    };

    public static BuffDebuffConfig BlessingOfHealingConfig = new BuffDebuffConfig
    {
        Id = 7,
        Name = "Blessing of Healing",
        Duration = 5,
        HealPerTick = 1
    };

    public static BuffDebuffConfig PowerOfMinotaurConfig = new BuffDebuffConfig
    {
        Id = 8,
        Name = "Power of Minotaur",
        Duration = 4,
        DamageChangedBy = 2
    };

    public static BuffDebuffConfig HasteConfig = new BuffDebuffConfig
    {
        Id = 9,
        Name = "Haste",
        Duration = 5,
        MovementSpeedChangedBy = 2.5f,
    };
}