using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots; // Reference to your slots

    public void AddWeaponToInventory(Weapon weapon)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.IsEmpty())
            {
                slot.AddItem(weapon); // Add the weapon to the empty slot
                Debug.Log("Weapon added to inventory: " + weapon.name);
                Debug.Log($"Checking if slot {slot.gameObject.name} is empty. Child count: {slot.transform.childCount}");
                return; // Exit after adding
            }
            else
            {
                Debug.Log("Slot is not empty: " + slot.name);
            }
        }
        Debug.Log("Inventory is full!"); // Log if no empty slots found
    }
}
