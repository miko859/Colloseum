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
            // disable the timeScale for a bit so the position can be changed
            float previousTimeScale = Time.timeScale;
            Time.timeScale = 1.0f;

            // update player position
            playerTransform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

            // restore time scale
            Time.timeScale = previousTimeScale;

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

