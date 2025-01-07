using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public Button RemoveButton;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        if (item != null)
        {
            Debug.Log($"Item added to InventoryItemController: {item.itemName}");
        }
    }


    public void UseItem()
    {
        if (item == null)
        {
            Debug.LogError("Cannot use item because 'item' is null.");
            return;
        }

        switch (item.itemType)
        {
            case Item.ItemType.Weapon:
                Debug.Log($"Equipping weapon: {item.itemName}");
                PickUpItem.Instance.EquipWeapon(item);
                break;
            case Item.ItemType.Armor:
                Debug.Log("Equipping armor.");
                break;
            case Item.ItemType.Helmet:
                Debug.Log("Equipping helmet.");
                break;
            case Item.ItemType.HealthPotion:
                Debug.Log("Using health potion.");
                break;
            case Item.ItemType.ManaPotion:
                Debug.Log("Using mana potion.");
                break;
            default:
                Debug.LogWarning("Unknown item type.");
                break;
        }

        RemoveItem();
    }

}
