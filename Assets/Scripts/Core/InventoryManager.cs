using System.Collections;
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
        // log the current state
        Debug.Log($"Clearing old inventory UI elements. Current count: {ItemContent.childCount}");

        // clear all inventory UI elements
        foreach (Transform child in ItemContent)
        {
            Destroy(child.gameObject);
        }

        // wait for destroy to complete before continuing
        StartCoroutine(RepopulateInventory());
    }

    private IEnumerator RepopulateInventory()
    {
        // wait for the end of the frame so that all elements are destroyed
        yield return new WaitForEndOfFrame();

        Debug.Log($"All old inventory elements cleared. Remaining count: {ItemContent.childCount}");

        // populate inventory with new items
        Debug.Log($"Populating inventory. Total items in list: {items.Count}");
        foreach (var item in items)
        {
            if (item != null)
            {
                GameObject obj = Instantiate(InventoryItem, ItemContent);

                var itemController = obj.GetComponent<InventoryItemController>();
                if (itemController != null)
                {
                    itemController.AddItem(item); // assign item to the controller
                }

                // sets UI elements
                var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
                var itemIcon = obj.transform.Find("ItemIcone").GetComponent<Image>();
                var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

                if (item.stackable)
                {
                    var itemCount = obj.transform.Find("ItemCount").GetComponent<TMP_Text>();

                    if (item is PotionData potionData)
                    {
                        itemCount.text = potionData.currentStack.ToString();
                    }
                    else if (item is Currency currency)
                    {
                        itemCount.text = currency.value.ToString();
                    }
                }

                itemName.text = item.itemName;
                itemIcon.sprite = item.icon;

                if (DeleteItems.isOn)
                {
                    removeButton.gameObject.SetActive(true);
                }
            }
        }


        // refresh inventory controllers
        SetInventoryItems();
    }


    private IEnumerator ClearDelayed()
    {
        yield return new WaitForEndOfFrame(); // wait for objects to be fully destroyed
        Debug.Log($"All old inventory elements cleared. Remaining child count: {ItemContent.childCount}");
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
        Debug.Log("Calling SetInventoryItems() to refresh inventory controllers.");

        // get all inventory item controllers from children
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        int validItemCount = 0;
        for (int i = 0; i < InventoryItems.Length; i++)
        {
            if (InventoryItems[i] != null && InventoryItems[i].item != null)
            {
                Debug.Log($"Valid inventory item added: {InventoryItems[i].item.itemName}");
                validItemCount++;
            }
            else
            {
                Debug.LogWarning("Found a missing or invalid inventory item controller.");
            }
        }

        Debug.Log($"InventoryItems updated: {validItemCount} valid items.");
    }



}
