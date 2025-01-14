

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Loot : Interactable

{
    public GameObject entity;
    public Item.LootRarity LootItemsRarity;
    public Item[] possibleLoot;
    public Item[] sureLoot;
    private LootDropRates lootDropRates;
    private Animator animator;
    

    [Header("What can be looted")]
    public bool itemDrop = true;
    public bool potionDrop = true;
    public bool moneyDrop = true;
    private bool looted = false;

    private void Start()
    {
        lootDropRates = GetComponent<LootDropRates>();
        animator = GetComponent<Animator>();
        interactPrompt = "Press E to loot";
    }

    public override void Interact()
    {
        if (!looted)
        {
            GenerateLoot();
            looted = !looted;
            interactPrompt = "";
        }
        if (animator != null)
        {
            animator.Play("Looting");
        }
        if (entity.CompareTag("DeadEnemy"))
        {
            StartCoroutine(DestroyEntity());
        }
    }

    private void GenerateLoot()
    {
        LootDropRates.LootDrop currentRates = GetRatesByRarity(LootItemsRarity);

        List<Item> lootedItems = new List<Item>();
        List<Item> lootPool = new List<Item>(possibleLoot);

        if (sureLoot.Length > 0)
        {
            foreach (Item item in sureLoot) 
            {
                if (item as WeaponData)
                {
                    lootedItems.Add(item);
                }
                else
                {
                    lootedItems.Add(CloneLootItem(item));
                }
                
            };
        }

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
                lootedItems.Add(CloneLootItem(entry));
                Debug.Log("potionsToDrop " +  potionsToDrop);
                potionsToDrop -= GetRarityIntValue(entry.rarity);
                Debug.Log("potion drop " + entry.itemName);
            }
        }
        if (moneyDrop)
        {
            int money = UnityEngine.Random.Range(currentRates.minMoney, currentRates.maxMoney + 1);
            Item currency = lootPool.Find(loot => loot.itemType == Item.ItemType.Currency);

            currency.value = money;
            lootedItems.Add(CloneLootItem(currency));
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
        Item tempItem; 
        
        do
        {
            tempItem = validLoot.ElementAt(UnityEngine.Random.Range(0, validLoot.Count));
        }
        while (itemsToDrop - GetRarityIntValue(tempItem.rarity) < 0);

        return tempItem;
        
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

    private Item CloneLootItem(Item original)
    {
        Item clone;
        if (original.itemType == Item.ItemType.Potion)
        {
            clone = ScriptableObject.CreateInstance<PotionData>();
        }
        else if (original.itemType == Item.ItemType.Currency)
        {
            clone = ScriptableObject.CreateInstance<Currency>();
        }
        else if (original.itemType == Item.ItemType.Miscellaneous)
        {
            clone = ScriptableObject.CreateInstance<MiscData>();
        }
        else
        {
            clone= ScriptableObject.CreateInstance<Item>();
        }

        clone.name = original.itemName;
        clone.itemName = original.itemName;
        clone.rarity = original.rarity;
        clone.stackable = original.stackable;
        clone.questItem = original.questItem;

        if (original is Currency currency)
        {
            Currency clonedCurrency = clone as Currency;
            if (clonedCurrency != null)
            {
                clonedCurrency.maxStack = currency.maxStack;
            }
        }
        else if (original is PotionData potion)
        {
            PotionData clonedPotion = clone as PotionData;
            if (clonedPotion != null)
            {
                clonedPotion.amount = potion.amount;
                clonedPotion.currentStack = potion.currentStack;
                clonedPotion.maxStack = potion.maxStack;
                clonedPotion.potionType = potion.potionType;
            }
        }

        clone.icon = original.icon;
        clone.value = original.value;
        clone.description = original.description;
        clone.itemType = original.itemType;

        clone.listOfEffects = new List<BuffDebuff>(original.listOfEffects);

        return clone;
    }

    private IEnumerator DestroyEntity()
    {
        yield return new WaitForSeconds(5);
        Destroy(entity);
    }
}