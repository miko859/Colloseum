using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private Weapon currentWeapon; // Hold the reference to the weapon
    public Text weaponNameText; // UI Text for displaying weapon name
    public Text weaponStatsText;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            Drag drag = dropped.GetComponent<Drag>();
            drag.parentAfterDrag = transform;
        }
    }

    public bool IsEmpty()
    {
        return currentWeapon == null; // Check if there's no weapon assigned
    }

    public void AddItem(Weapon weapon)
    {
        if (currentWeapon == null) // Only add if the slot is empty
        {
            currentWeapon = weapon; // Store the reference
            GameObject weaponObject = Instantiate(weapon.gameObject, transform);
            weaponObject.transform.localPosition = Vector3.zero; // Center the item in the slot

            // Update the stats text
            UpdateWeaponStats(weapon);
            Debug.Log($"Added weapon: {weapon.weaponData.weaponName} to slot: {gameObject.name}");
        }
        else
        {
            Debug.LogWarning("Slot is already occupied!");
        }
    }

    private void UpdateWeaponStats(Weapon weapon)
    {
        if (weaponStatsText != null && weapon.weaponData != null)
        {
            weaponStatsText.text = $"{weapon.weaponData.weaponName}\n" +
                                    $"Light Damage: {weapon.weaponData.lightAttackDamage}\n" +
                                    $"Heavy Damage: {weapon.weaponData.heavyAttackDamage}";
        }
        else
        {
            Debug.LogWarning("WeaponStatsText or weaponData is null");
        }
    }

    public void ClearSlot()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject); // Optionally destroy the weapon object
            currentWeapon = null; // Clear the reference
        }
        weaponNameText.text = ""; // Clear the name text
        weaponStatsText.text = ""; // Clear the stats text
    }
}
