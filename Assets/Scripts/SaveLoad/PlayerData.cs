using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public double healthData;
    public float[] position;
    public List<string> equippedWeapons; // List to store weapon names

    public PlayerData(Transform playerTransform, HealthBar healthBar, EquipedWeaponManager equipedWeaponManager)
    {
        if (playerTransform == null)
        {
            throw new ArgumentNullException(nameof(playerTransform), "Player transform cannot be null");
        }
        if (healthBar == null)
        {
            throw new ArgumentNullException(nameof(healthBar), "Health bar cannot be null");
        }
        if (equipedWeaponManager == null)
        {
            throw new ArgumentNullException(nameof(equipedWeaponManager), "EquipedWeaponManager cannot be null");
        }

        healthData = healthBar.currentHealth;

        position = new float[3];
        position[0] = playerTransform.position.x;
        position[1] = playerTransform.position.y;
        position[2] = playerTransform.position.z;

        equippedWeapons = new List<string>();
        foreach (var weapon in equipedWeaponManager.weaponery)
        {
            if (weapon == null)
            {
                Debug.LogError("Null weapon found in weaponery!");
            }
            else
            {
                Debug.Log($"Weapon in weaponery: {weapon.weaponName}");
                equippedWeapons.Add(weapon.weaponName);
            }
        }

    }
}
