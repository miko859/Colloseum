using System;
using UnityEngine;

public class LootDropRates : MonoBehaviour
{
    [Serializable]
    public class LootDrop
    {
        public int minMoney;
        public int maxMoney;
        public int minPotions;
        public int maxPotions;
        public int minItems;
        public int maxItems;
    }

    public LootDrop commonRates = new LootDrop
    {
        minMoney = 5,
        maxMoney = 15,
        minPotions = 0,
        maxPotions = 2,
        minItems = 0,
        maxItems = 2
    };

    public LootDrop uncommonRates = new LootDrop
    {
        minMoney = 10,
        maxMoney = 25,
        minPotions = 1,
        maxPotions = 3,
        minItems = 1,
        maxItems = 3
    };

    public LootDrop rareRates = new LootDrop
    {
        minMoney = 20,
        maxMoney = 40,
        minPotions = 1,
        maxPotions = 3,
        minItems = 2,
        maxItems = 4
    };

    public LootDrop epicRates = new LootDrop
    {
        minMoney = 50,
        maxMoney = 60,
        minPotions = 2,
        maxPotions = 3,
        minItems = 4,
        maxItems = 5
    };

    public LootDrop legendaryRates = new LootDrop
    {
        minMoney = 70,
        maxMoney = 80,
        minPotions = 4,
        maxPotions = 4,
        minItems = 5,
        maxItems = 7
    };

    public LootDrop mysteriousRates = new LootDrop
    {
        minMoney = 80,
        maxMoney = 90,
        minPotions = 5,
        maxPotions = 6,
        minItems = 8,
        maxItems = 10
    };
}