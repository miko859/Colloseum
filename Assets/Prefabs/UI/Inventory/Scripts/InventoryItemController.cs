using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public  Item item;

    public Button RemoveButton;
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Weapon:
                PickUpItem.Instance.EquipWeapon();
                break;
            case Item.ItemType.Armor:
                break;
            case Item.ItemType.Helmet:
                break;
            case Item.ItemType.HealthPotion:
                break;
            case Item.ItemType.ManaPotion:
                break;
            default:
                break;
        }
        RemoveItem();
    }
}
