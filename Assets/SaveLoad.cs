using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public Transform playerTransform; 
    public HealthBar healthBar;       

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(playerTransform, healthBar);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            // update player position
            playerTransform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

            // update player health 
            healthBar.SetHealth(data.healthData, true);

            Debug.Log($"Loaded Position: {playerTransform.position}, Health: {healthBar.currentHealth}");
        }
        else
        {
            Debug.LogError("No data to load.");
        }
    }
}

