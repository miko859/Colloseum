using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int value;
    public string description;
    public ItemType itemType;
    List<BuffDebuff> listOfEffects = new List<BuffDebuff>();

    public enum ItemType
    {
        Weapon,
        Armor,
        Helmet,
        HealthPotion,
        ManaPotion
    }
}
