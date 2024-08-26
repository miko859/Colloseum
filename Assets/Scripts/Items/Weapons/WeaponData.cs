using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float lightAttackDuration;
    public float heavyAttackChargeDuration;
    public float heavyAttackPerformDuration;
    public float bashDuration;

    public string lightAttackAnimation;
    public string heavyAttackAnimation;
    public string heavyAttackPerformAnimation;
    public string blockAnimation;
    public string bashAnimation;

    public int lightAttackDamage;
    public int heavyAttackDamage;
}