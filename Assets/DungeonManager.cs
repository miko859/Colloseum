using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{

    public GameObject[] enemies;
    public GameObject victoryScreen;
    private int totalEnemies;
    public static int deadEnemies = 0;
    bool showingVictoryScreen = false;
    bool stopShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;
        victoryScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemies();
        if (Input.anyKey && showingVictoryScreen==true && stopShowing==true)
        {
            victoryScreen.active = false;
            stopShowing = false;

        }
    }

    public void CheckEnemies()
    {
        int deadEnemies = 0;
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null&&enemy.tag == "DeadEnemy")
            {
                deadEnemies++;
                Debug.Log("Dead: "+ deadEnemies); 
            }

        }

        if (deadEnemies >= totalEnemies&& showingVictoryScreen == false)
        {
            ShowVictoryScreen();
            Debug.Log("Showing Victory Screen");
        }
    }

    private void ShowVictoryScreen()
    {
        StartCoroutine(wait());

        victoryScreen.SetActive(true);
        
        showingVictoryScreen=true;

    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        stopShowing = true;
    }
}
