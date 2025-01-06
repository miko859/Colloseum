using UnityEngine;

[CreateAssetMenu(fileName = "PotionData", menuName = "Items/PotionData", order = 3)]
public class PotionData : Item
{
    public PotionType potionType;
    public double amount;
}

public enum PotionType
{
    HEALTH,
    MANA
}