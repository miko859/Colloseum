using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/EnemyData", order = 2)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float enemyHealth;

    public float lightAttackDuration;
    public float heavyAttackChargeDuration;
    public float heavyAttackPerformDuration;
    public float bashDuration;

    public string lightAttackAnimation;
    public string heavyAttackAnimation;
    public string heavyAttackPerformAnimation;
    public string blockAnimation;
    public string bashAnimation;

    public float lightAttackDamage;
    public float heavyAttackDamage;
}