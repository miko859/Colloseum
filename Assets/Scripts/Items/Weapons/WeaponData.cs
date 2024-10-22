using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    
    public string weaponName;
    public int lightAttackTypesCount;
    [Header("Animation duration")]
    public float lightAttackDuration1;
    public float lightAttackDuration2;
    public float lightAttackDuration3;
    public float heavyAttackChargeDuration;
    public float heavyAttackPerformDuration;
    public float bashDuration;
    public float hurtDuration;
    public float equipUnequipDuration;
    [Header("Animation names")]
    public string lightAttackAnimation;
    public string heavyAttackAnimation;
    public string heavyAttackPerformAnimation;
    public string blockAnimation;
    public string bashAnimation;
    public string hurtAnimation;
    [Header("Weapon stats")]
    public int lightAttackDamage;
    public int heavyAttackDamage;
}