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

}

