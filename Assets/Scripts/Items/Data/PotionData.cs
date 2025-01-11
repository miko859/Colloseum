using UnityEngine;

[CreateAssetMenu(fileName = "PotionData", menuName = "Items/PotionData", order = 3)]
public class PotionData : Item
{
    public PotionType potionType;
    public float amount;
    public int currentStack = 1;
    public int maxStack = 5;
}

public enum PotionType
{
    HEALTH,
    MANA
}