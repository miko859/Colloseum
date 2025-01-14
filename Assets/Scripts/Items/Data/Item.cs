using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int value;
    public string description;
    public ItemType itemType;
    public LootRarity rarity;
    public bool stackable = false;
    public bool questItem = false;
    public List<BuffDebuff> listOfEffects = new List<BuffDebuff>();

    public enum ItemType
    {
        Equipment,
        Potion,
        Currency,
        Miscellaneous
    }
    public enum LootRarity
    {
        COMMON,
        UNCOMMON,
        RARE,
        EPIC,
        LEGENDARY,
        MYSTERIOUS
    }
}
