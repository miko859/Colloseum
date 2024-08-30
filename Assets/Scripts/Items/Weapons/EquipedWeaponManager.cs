using System.Collections.Generic;
using UnityEngine;

public class EquipedWeaponManager : MonoBehaviour
{
    public static EquipedWeaponManager Instance;

    public List<Weapon> weaponery = new List<Weapon>();
    private int currentWeaponIndex = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// This will equip weapon from inventory based on index recevied from InputSystem
    /// </summary>
    /// <param name="index"></param>
    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < weaponery.Count)
        {
            // Deaktivuj súèasnú zbraò
            if (currentWeaponIndex != -1)
            {
                weaponery[currentWeaponIndex].Unequip();
            }

            // Aktivuj novú zbraò
            currentWeaponIndex = index;
            weaponery[currentWeaponIndex].Equip();
        }
    }

    
    /// <summary>
    /// Add weapon into weaponery/inventory
    /// </summary>
    /// <param name="newWeapon"></param>
    public void AddWeapon(Weapon newWeapon)
    {
        weaponery.Add(newWeapon);
        
        Debug.Log("Weapon added: " + newWeapon.name);
    }
}