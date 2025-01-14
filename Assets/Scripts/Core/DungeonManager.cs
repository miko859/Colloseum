using System.Collections;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{

    public GameObject[] enemies;
    public GameObject victoryScreen;
    private int totalEnemies;
    bool showingVictoryScreen = false;
    bool stopShowing = false;
    public GameObject bossEntity;
    public GameObject bossDoor;
    public GameObject exitDoor;

    void Start()
    {
        totalEnemies = enemies.Length;
        victoryScreen.SetActive(false);
    }

    void Update()
    {
        if (CheckEnemies())
        {
            bossDoor.GetComponent<DoorScript>().OpenDoor();
        }

        if (CheckBoss())
        {
            exitDoor.GetComponent<DoorScript>().OpenDoor();
        }
    }

    public bool CheckEnemies()
    {
        int deadEnemies = 0;
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null && enemy.tag == "DeadEnemy")
            {
                deadEnemies++;
            }
        }

        if (deadEnemies >= totalEnemies)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckBoss()
    {
        if (bossEntity.GetComponent<Health>().GetCurrentHealth() <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}