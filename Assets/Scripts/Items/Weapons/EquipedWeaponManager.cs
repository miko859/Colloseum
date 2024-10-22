using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EquipedWeaponManager : MonoBehaviour
{
    public static EquipedWeaponManager Instance;

    public List<Weapon> weaponery = new List<Weapon>();
    private int currentWeaponIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public int getCurrentWeaponIndex() 
    {  
        return currentWeaponIndex; 
    }

    /// <summary>
    /// This will equip weapon from inventory based on index recevied from InputSystem
    /// </summary>
    /// <param name="index"></param>
    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < weaponery.Count)
        {
            currentWeaponIndex = index;

            StartCoroutine(transform.GetComponent<PlayerController>().EquipWeapon(weaponery[currentWeaponIndex]));
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