using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public double healthData;
    public float[] position;

    public PlayerData(Transform playerTransform, HealthBar healthBar)
    {
        healthData = healthBar.currentHealth;

        position = new float[3];
        position[0] = playerTransform.position.x;
        position[1] = playerTransform.position.y;
        position[2] = playerTransform.position.z;
    }
}

