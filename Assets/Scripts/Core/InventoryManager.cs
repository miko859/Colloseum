using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<Item> items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle DeleteItems;
    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        if (item.stackable)
        {
            if (item.itemType == Item.ItemType.Currency)
            {
                Currency tempItem = items.Find(currency => currency.itemType == item.itemType && currency.value < (item as Currency).maxStack) as Currency;
                if (tempItem != null)
                {
                    int spaceLeft = (item as Currency).maxStack - tempItem.value;
                    int difference = spaceLeft - item.value;

                    if (difference >= 0)
                    {
                        tempItem.value += item.value;
                    }
                    else
                    {
                        tempItem.value = (item as Currency).maxStack;
                        item.value -= spaceLeft;
                        items.Add(item);
                    }
                }
                else
                {
                    items.Add(item);
                }
                
            }
            else if (item.itemType == Item.ItemType.Potion)
            {
                Item tempItem = items.Find(potion => potion.itemName.Equals(item.itemName) && (potion as PotionData).currentStack < (potion as PotionData).maxStack);
                if (tempItem != null)
                {
                    if ((tempItem as PotionData).currentStack < (tempItem as PotionData).maxStack)
                    {
                        (tempItem as PotionData).currentStack += 1;
                    }
                    else
                    {
                        items.Add(item);
                    }
                }
                else
                {
                    items.Add(item);
                }
                
                
            }
        }
        else
        {
            items.Add(item);
        }
        
        Debug.Log($"{item.itemName} added to inventory.");
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        Debug.Log($"{item.itemName} removed from inventory.");
    }

    public void ListItems()
    {
        foreach (Transform child in ItemContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcone").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (DeleteItems.isOn)
            {
                removeButton.gameObject.SetActive(true);
                
            }
        }
       SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (DeleteItems.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < items.Count; i++)
        {
            InventoryItems[i].AddItem(items[i]);
        }
    }
}
