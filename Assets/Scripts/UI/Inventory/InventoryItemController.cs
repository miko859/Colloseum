using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public Button RemoveButton;
    private GameObject player;

    private void Start()
    {
        player = FindObjectOfType<CharacterController>().GameObject();
    }

    public void RemoveItem()
    {
        //if (!item.questItem)
        //{
            InventoryManager.Instance.Remove(item);
            Destroy(gameObject);
        //}
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
            case Item.ItemType.Equipment:
                if (item as WeaponData)
                {
                    PickUpItem.Instance.EquipWeapon(item);
                    RemoveItem();
                }
                break;
            case Item.ItemType.Potion:
                var itemCount = transform.Find("ItemCount").GetComponent<TMP_Text>();

                if ((item is PotionData healingPotion) && healingPotion.potionType == PotionType.HEALTH)
                {
                    player.GetComponent<Health>().Heal(healingPotion.amount);
                    healingPotion.currentStack -= 1;
                    itemCount.text = healingPotion.currentStack.ToString();

                    if (healingPotion.currentStack == 0)
                    {
                        RemoveItem();
                    }
                    Debug.Log("Heal");
                }
                else if ((item is PotionData manaPotion) && manaPotion.potionType == PotionType.MANA)
                {
                    player.GetComponentInChildren<PlayerController>().manaSystem.AddMana(manaPotion.amount);
                    manaPotion.currentStack -= 1;
                    itemCount.text = manaPotion.currentStack.ToString();

                    if (manaPotion.currentStack == 0)
                    {
                        RemoveItem();
                    }
                    Debug.Log("Mana");
                }
                break;
            case Item.ItemType.Currency:
                Debug.Log($"{item.value} gold coins");
                break;
            case Item.ItemType.Miscellaneous:
                Debug.Log($"{item.name} miscellaneous");
                break;
            default:
                Debug.LogWarning("Unknown item type.");
                break;
        }
    }
}