using UnityEngine;
using System.Linq;

public class SaveLoad : MonoBehaviour
{
    public Transform playerTransform;
    public HealthBar healthBar;
    public EquipedWeaponManager equipedWeaponManager;

    private void Awake()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
        if (healthBar == null)
        {
            healthBar = GameObject.FindObjectOfType<HealthBar>();
        }
        if (equipedWeaponManager == null)
        {
            equipedWeaponManager = GameObject.FindObjectOfType<EquipedWeaponManager>();
        }
    }

    public void SavePlayer()
    {
        if (playerTransform == null || healthBar == null || equipedWeaponManager == null)
        {
            Debug.LogError("SavePlayer: One or more required components are not assigned.");
            return;
        }

        // Save player data (position, health, and weapons)
        SaveSystem.SavePlayer(playerTransform, healthBar, equipedWeaponManager);

        // Call SavePlayerData to log the weapons
        SavePlayerData();
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            // Disable rigidBody physics and movement logic temporarily
            Rigidbody rb = playerTransform.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;

            CharacterController controller = playerTransform.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;

            // Restore player position
            playerTransform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            Debug.Log($"Player position updated to: {playerTransform.position}");

            // Restore health
            healthBar.SetHealth(data.healthData, true);
            Debug.Log($"Player health updated to: {data.healthData}");

            // Clear existing equipped weapons but do not delete hands_model
            foreach (var weapon in equipedWeaponManager.weaponery)
            {
                if (weapon != null && weapon.gameObject.name != "hands_model") // Skip deleting hands_model
                {
                    Destroy(weapon.gameObject); // Destroy weapon GameObject
                }
            }
            equipedWeaponManager.weaponery.Clear();

            // Ensure hands_model is added as a weapon if it was previously equipped
            Weapon handsWeapon = equipedWeaponManager.FindOrCreateWeapon("hands_model");
            if (handsWeapon != null)
            {
                equipedWeaponManager.weaponery.Add(handsWeapon); // Add hands to the list if not already
            }

            // Load equipped weapons from saved data
            foreach (var weaponName in data.equippedWeapons)
            {
                // Skip adding hands_model as it should always be in the weapon list
                if (weaponName != "hands_model")
                {
                    Weapon weapon = equipedWeaponManager.FindOrCreateWeapon(weaponName);
                    if (weapon != null)
                    {
                        equipedWeaponManager.weaponery.Add(weapon); // Add weapon to the list
                    }
                    else
                    {
                        Debug.LogError($"Failed to load weapon: {weaponName}");
                    }
                }
            }

            Debug.Log("Before: " + equipedWeaponManager.weaponery.Count);
            equipedWeaponManager.RemoveOddIndexedItems();
            Debug.Log("After: " + equipedWeaponManager.weaponery.Count);

            // Disable all weapons except the first one in the list
            for (int i = 0; i < equipedWeaponManager.weaponery.Count; i++)
            {
                if (i == 0)
                {
                    equipedWeaponManager.weaponery[i].gameObject.SetActive(true); // Enable the first weapon
                }
                else
                {
                    equipedWeaponManager.weaponery[i].gameObject.SetActive(false); // Disable all other weapons
                }
            }

            // Enable rigidBody physics and movement logic
            if (rb != null) rb.isKinematic = false;
            if (controller != null) controller.enabled = true;

            Debug.Log($"Loaded Position: {playerTransform.position}, Health: {healthBar.currentHealth}, Weapons: {string.Join(", ", data.equippedWeapons)}");
        }
        else
        {
            Debug.LogError("No data to load.");
        }
    }





    private void SavePlayerData()
    {
        Debug.Log("Saving Player Data...");

        // log weapon data
        foreach (var weapon in equipedWeaponManager.weaponery)
        {
            if (weapon != null)
            {
                Debug.Log($"Weapon in weaponery: {weapon.weaponName}");
            }
            else
            {
                Debug.LogWarning("Null weapon detected in weaponery!");
            }
        }

        
        string weaponsList = string.Join(", ", equipedWeaponManager.weaponery.Select(w => w.weaponName));
        Debug.Log($"Saving Player Data: Position = {playerTransform.position}, Health = {healthBar.currentHealth}, Weapons = {weaponsList}");

        //saves the data to the system using SaveSystem
        SaveSystem.SavePlayer(playerTransform, healthBar, equipedWeaponManager);
    }
}
