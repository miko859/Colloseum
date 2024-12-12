using System.Collections.Generic;
using UnityEngine;

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

    public Weapon FindOrCreateWeapon(string weaponName)
    {
        
        Weapon weapon = weaponery.Find(w => w.weaponName == weaponName);

        if (weapon != null)
        {
            Debug.Log($"Restored existing weapon: {weapon.weaponName}");
        }
        else
        {
            // load weapon data
            Debug.Log($"Loading WeaponData for: {weaponName}");
            WeaponData weaponData = Resources.Load<WeaponData>($"Weapons/{weaponName}");

            if (weaponData != null)
            {
                // creates a weapon instance
                weapon = Instantiate(weaponData.weaponPrefab);
                weapon.weaponName = weaponName; // Set weapon name

                Debug.Log($"Created weapon instance: {weapon.weaponName}");

                // Get player's body transform (or hand if available)
                Transform bodyTransform = transform.GetComponent<PlayerController>().GetBodyTransform(); 

                if (bodyTransform != null)
                {
                    // parent weapon to the player body
                    weapon.transform.SetParent(bodyTransform, false); 
                    weapon.transform.localPosition = Vector3.zero; 
                    weapon.transform.localRotation = Quaternion.identity;
                    weapon.transform.localScale = Vector3.one; 

                    // reset position rotation and scale of axe_rig
                    Transform axeRig = weapon.transform.Find("axe_rig"); 
                    if (axeRig != null)
                    {
                        axeRig.localPosition = Vector3.zero; 
                        axeRig.localRotation = Quaternion.identity; 
                        axeRig.localScale = Vector3.one; 
                    }
                    else
                    {
                        Debug.LogError("axe_rig not found in weapon.");
                    }

                    // Add weapon to weaponery
                    AddWeapon(weapon);

                    Debug.Log($"Added new weapon to weaponery: {weapon.weaponName}");
                }
                else
                {
                    Debug.LogError("Body transform not found! Ensure that the player's body is correctly named.");
                    return null;
                }
            }
            else
            {
                Debug.LogError($"WeaponData not found for: {weaponName}");
                return null;
            }
        }

        return weapon;
    }











    /// <summary>
    /// Add weapon into weaponery/inventory
    /// </summary>
    /// <param name="newWeapon"></param>
    public void AddWeapon(Weapon newWeapon)
    {
        if (string.IsNullOrEmpty(newWeapon.weaponName))
        {
            Debug.LogError("Weapon name is empty! This weapon will not be saved correctly.");
        }

        if (!weaponery.Contains(newWeapon))
        {
            weaponery.Add(newWeapon);
            Debug.Log($"Weapon added to weaponery: {newWeapon.weaponName} ({newWeapon.GetType().Name})");
        }
        else
        {
            Debug.LogWarning($"Weapon already exists in weaponery: {newWeapon.weaponName}");
        }

        // Temporary fix for equipping first weapon
        if (transform.GetComponent<PlayerController>().currentWeapon == null)
        {
            StartCoroutine(transform.GetComponent<PlayerController>().EquipWeapon(weaponery[0]));
        }
    }

    public void LoadWeapon(string weaponName)
    {
        // Find or create the weapon from the weaponery (inventory)
        Weapon weapon = FindOrCreateWeapon(weaponName);

        if (weapon != null)
        {
            // Switch to this weapon if it's successfully loaded
            SwitchWeapon(weaponery.IndexOf(weapon));
        }
        else
        {
            Debug.LogError("Failed to load weapon: " + weaponName);
        }
    }


}