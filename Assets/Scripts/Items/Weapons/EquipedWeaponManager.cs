using System.Collections.Generic;
using UnityEngine;

public class EquipedWeaponManager : MonoBehaviour
{
    public static EquipedWeaponManager Instance;

    public List<Weapon> weaponery = new List<Weapon>();
    private int currentWeaponIndex = 0;

    [SerializeField]
    private GameObject handsPrefab; // The hands prefab to be added

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //handsPrefab is added to weaponery at startup
       // AddHandsToWeaponery();
    }

    public int getCurrentWeaponIndex()
    {
        return currentWeaponIndex;
    }

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
            Debug.Log($"Loading WeaponData for: {weaponName}");
            WeaponData weaponData = Resources.Load<WeaponData>($"Weapons/{weaponName}");

            if (weaponData != null)
            {
                weapon = Instantiate(weaponData.weaponPrefab);
                weapon.weaponName = weaponName;

                Debug.Log($"Created weapon instance: {weapon.weaponName}");

                Transform bodyTransform = transform.GetComponent<PlayerController>().GetBodyTransform();

                if (bodyTransform != null)
                {
                    weapon.transform.SetParent(bodyTransform, false);
                    weapon.transform.localPosition = Vector3.zero;
                    weapon.transform.localRotation = Quaternion.identity;
                    weapon.transform.localScale = Vector3.one;

                    if (!weaponery.Contains(weapon))
                    {
                        AddWeapon(weapon);
                        Debug.Log($"Added new weapon to weaponery: {weapon.weaponName}");
                    }
                    else
                    {
                        Debug.Log($"Weapon already in weaponery: {weapon.weaponName}");
                    }
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

        AddHandsToWeaponery(); // Ensure handsPrefab is always at the end
    }

    public void LoadWeapon(string weaponName)
    {
        Weapon weapon = FindOrCreateWeapon(weaponName);

        if (weapon != null)
        {
            SwitchWeapon(weaponery.IndexOf(weapon));
        }
        else
        {
            Debug.LogError("Failed to load weapon: " + weaponName);
        }
    }

    public void RemoveOddIndexedItems()
    {
        for (int i = weaponery.Count - 1; i >= 0; i--)
        {
            if (i % 2 != 0)
            {
                weaponery.RemoveAt(i);
            }
        }

        AddHandsToWeaponery(); // Ensure handsPrefab is always at the end
    }

    private void AddHandsToWeaponery()
    {
        if (handsPrefab != null)
        {
            Weapon handsWeapon = handsPrefab.GetComponent<Weapon>();

            if (handsWeapon != null && !weaponery.Contains(handsWeapon))
            {
                weaponery.Add(handsWeapon);
                Debug.Log("Hands prefab added to weaponery.");
            }
        }
        else
        {
            Debug.LogWarning("Hands prefab is not assigned in the inspector.");
        }
    }
}
