public static class BuffDebuffData
{
    public static BuffDebuffConfig BurningConfig = new BuffDebuffConfig
    {
        Name = "Burning",
        DamagePerTick = 2,
        Frequency = 1f,
        Duration = 5f
    };

    public static BuffDebuffConfig FreezingConfig = new BuffDebuffConfig
    {
        Name = "Freezing",
        MovementSpeedChangedBy = -30,
        Duration = 3f
    };
}