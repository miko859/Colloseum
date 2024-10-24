
public class BuffDebuffConfig
{
    public enum DamageType { Physical, Magical };

    public string Name { get; set; }
    public DamageType DmgType { get; set; }
    public string Description { get; set; }
    public float Duration { get; set; }
    public float Frequency { get; set; }
    public int DamagePerTick { get; set; }
    public int HealPerTick { get; set; }
    public int MovementSpeedChangedBy { get; set; }
    public int DamageChangedBy { get; set; }
    public bool DisableSpellsSkills { get; set; }
}