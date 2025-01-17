using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Items/WeaponData", order = 1)]
public class WeaponData : Item
{
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
    public float blockedDuration;

    [Header("Animation names")]
    public string lightAttackAnimation;
    public string heavyAttackAnimation;
    public string heavyAttackPerformAnimation;
    public string blockAnimation;
    public string bashAnimation;
    public string hurtAnimation;
    public string blockedAnimation;

    [Header("Weapon stats")]
    public float lightAttackDamage;
    public float heavyAttackDamage;
    public float blockTreshhold;
    [Header("Stamina Consumption")]
    public float heavyAttackStaminaCons;
    public float bashStaminaCons;
    public float blockStaminaCons;
    public Weapon weaponPrefab;

    public string weaponName; 
}