using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        items.Add(item);
        Debug.Log($"{item.itemName} added to inventory.");
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        Debug.Log($"{item.itemName} removed from inventory.");
    }
}
