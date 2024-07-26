using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject victoryScreen;
    private int totalEnemies;
    // Start is called before the first frame update
    void Start()
    {
        totalEnemies = enemies.Length;
        victoryScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemies();
    }

    public void CheckEnemies()
    {
        int deadEnemies = 0;
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                deadEnemies++;
            }

        }

        if (deadEnemies>= totalEnemies)
        {
           ShowVictoryScreen();
        }
    }

    private void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
    }
}
