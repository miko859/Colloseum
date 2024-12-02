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
            Debug.Log($"Loaded Player Data: Position = ({data.position[0]}, {data.position[1]}, {data.position[2]}), Health = {data.healthData}");

            playerTransform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            healthBar.currentHealth = (float)data.healthData;
        }
        else
        {
            Debug.LogError("Failed to load player data.");
        }
    }
}

