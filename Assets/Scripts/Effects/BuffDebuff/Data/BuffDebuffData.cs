public static class BuffDebuffData
{
    public static BuffDebuffConfig BurningConfig = new BuffDebuffConfig
    {
        Name = "Burning",
        DamagePerTick = 5,
        Frequency = 1f,
        Duration = 10f
    };

    public static BuffDebuffConfig FreezingConfig = new BuffDebuffConfig
    {
        Name = "Freezing",
        MovementSpeedChangedBy = -30,
        Duration = 3f
    };
}