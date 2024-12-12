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
            // disable rigidBody physics
            Rigidbody rb = playerTransform.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;

            // disable movement logic
            CharacterController controller = playerTransform.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;

            // disable the timeScale for a bit so the position can be changed
            float previousTimeScale = Time.timeScale;
            Time.timeScale = 1.0f;

            // update player position
            playerTransform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            Debug.Log($"Player position updated to: {playerTransform.position}");

            // restore time scale
            Time.timeScale = previousTimeScale;

            // update player health
            healthBar.SetHealth(data.healthData, true);

            // Load equipped weapons
            equipedWeaponManager.weaponery.Clear();
            foreach (var weaponName in data.equippedWeapons)
            {
                Weapon weapon = equipedWeaponManager.FindOrCreateWeapon(weaponName);
                equipedWeaponManager.weaponery.Add(weapon); // Add the weapon to the list
            }

            // enable the logic we disabled earlier
            if (rb != null) rb.isKinematic = false;
            if (controller != null) controller.enabled = true;

            Debug.Log($"Loaded Position: {playerTransform.position}, Health: {healthBar.currentHealth}");
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
