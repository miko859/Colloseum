

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Loot : Interactable

{
    public Item.LootRarity LootItemsRarity;
    public Item[] possibleLoot;
    private LootDropRates lootDropRates;

    [Header("What can be looted")]
    public bool itemDrop = true;
    public bool potionDrop = true;
    public bool moneyDrop = true;

    private void Start()
    {
        lootDropRates = GetComponent<LootDropRates>();
    }

    public override void Interact()
    {
        GenerateLoot();
    }

    private void GenerateLoot()
    {
        LootDropRates.LootDrop currentRates = GetRatesByRarity(LootItemsRarity);

        List<Item> lootedItems = new List<Item>();
        List<Item> lootPool = new List<Item>(possibleLoot);

        if (itemDrop)
        {
            int itemsToDrop = UnityEngine.Random.Range(currentRates.minItems, currentRates.maxItems + 1);

            while (itemsToDrop != 0)
            {
                Item entry = GetRandomLootByRarity(lootPool, itemsToDrop, Item.ItemType.Equipment);
                lootedItems.Add(entry);
                Debug.Log("Items to drop " + itemsToDrop);
                itemsToDrop -= GetRarityIntValue(entry.rarity);
                Debug.Log("equipment drop " + entry.itemName);
            }
        }
        if (potionDrop)
        {
            int potionsToDrop = UnityEngine.Random.Range(currentRates.minPotions, currentRates.maxPotions + 1);

            while (potionsToDrop != 0)
            {
                Item entry = GetRandomLootByRarity(lootPool, potionsToDrop, Item.ItemType.Potion);
                lootedItems.Add(entry);
                Debug.Log("potionsToDrop " +  potionsToDrop);
                potionsToDrop -= GetRarityIntValue(entry.rarity);
                Debug.Log("potion drop " + entry.itemName);
            }
        }
        if (moneyDrop)
        {
            int money = UnityEngine.Random.Range(currentRates.minMoney, currentRates.maxMoney + 1);
            Item currency = new Currency();
            currency.value = money;
            lootedItems.Add(currency);
            Debug.Log("money drop " +  currency.value);
        }
        
        AddLootIntoInventory(lootedItems);
    }

    private void AddLootIntoInventory(List<Item> lootedItems)
    {
        foreach (Item loot in lootedItems)
        {
            InventoryManager.Instance.Add(loot);
        }
    }

    private int AdjustAmountByChestRarity(int baseAmount)
    {
        switch (LootItemsRarity)
        {
            case Item.LootRarity.COMMON: return baseAmount;
            case Item.LootRarity.UNCOMMON: return Mathf.CeilToInt(baseAmount * 1.1f);
            case Item.LootRarity.RARE: return Mathf.CeilToInt(baseAmount * 1.25f);
            case Item.LootRarity.EPIC: return Mathf.CeilToInt(baseAmount * 1.5f);
            case Item.LootRarity.LEGENDARY: return Mathf.CeilToInt(baseAmount * 2f);
            case Item.LootRarity.MYSTERIOUS: return Mathf.CeilToInt(baseAmount * 3f);
            default: return baseAmount;
        }
    }

    private bool IsValidLoot(Item.LootRarity rarity)
    {
        if (rarity <= LootItemsRarity)
        {
            return true;
        }
        else if (rarity == LootItemsRarity + 1)
        {
            float chance = 0.05f;
            return UnityEngine.Random.value < chance;
        }
        return false;
    }

    private LootDropRates.LootDrop GetRatesByRarity(Item.LootRarity rarity)
    {
        switch (rarity)
        {
            case Item.LootRarity.COMMON: return lootDropRates.commonRates;
            case Item.LootRarity.UNCOMMON: return lootDropRates.uncommonRates;
            case Item.LootRarity.RARE: return lootDropRates.rareRates;
            case Item.LootRarity.EPIC: return lootDropRates.epicRates;
            case Item.LootRarity.LEGENDARY: return lootDropRates.mysteriousRates;
            default: return lootDropRates.commonRates;
        }
    }
    private Item GetRandomLootByRarity(List<Item> lootPool, int itemsToDrop, Item.ItemType itemType)
    {
        List<Item> validLoot = lootPool.FindAll(loot => GetRarityIntValue(loot.rarity) <= GetRarityIntValue(LootItemsRarity) + 1).Where(loot => loot.itemType == itemType).ToList<Item>();
        if (validLoot.Count == 0)
            return null;
        Item tempItem = validLoot[UnityEngine.Random.Range(0, validLoot.Count)];

        if (itemsToDrop - GetRarityIntValue(tempItem.rarity) > -1)
        {
            return tempItem;
        }
        else
        {
            GetRandomLootByRarity(lootPool, itemsToDrop, itemType);
            return null;
        }
    }

    private int GetRarityIntValue(Item.LootRarity rarity)
    {
        switch (rarity)
        {
            case Item.LootRarity.COMMON: return 1;
            case Item.LootRarity.UNCOMMON: return 2;
            case Item.LootRarity.RARE: return 3;
            case Item.LootRarity.EPIC: return 4;
            case Item.LootRarity.LEGENDARY: return 5;
            default: return 1;
        }
    }
}