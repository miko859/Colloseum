using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD,
        HARDCORE
    }

    public static DifficultyManager Instance { get; set; }

    public Difficulty CurrentDifficulty { get; private set; } = Difficulty.MEDIUM;

    private Dictionary<Difficulty, float> enemyDamageMultiplier = new Dictionary<Difficulty, float>
    {
        { Difficulty.EASY, 0.75f },
        { Difficulty.MEDIUM, 1.0f },
        { Difficulty.HARD, 1.25f }
    };

    private Dictionary<Difficulty, float> enemyHealthMultiplier = new Dictionary<Difficulty, float>
    {
        { Difficulty.EASY, 0.8f },
        { Difficulty.MEDIUM, 1.0f },
        { Difficulty.HARD, 1.2f }
    };

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
        Debug.Log("Difficulty before load: " + CurrentDifficulty);
        LoadDifficulty();
        Debug.Log("Difficulty after load: " + CurrentDifficulty);
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        CurrentDifficulty = difficulty;
    }

    public float GetEnemyDamageMultiplier()
    {
        return enemyDamageMultiplier[CurrentDifficulty];
    }

    public float GetEnemyHealthMultiplier()
    {
        return enemyHealthMultiplier[CurrentDifficulty];
    }

    private void LoadDifficulty()
    {
        int value = Settings.Get<int>("DIFFICULTY");
        Debug.Log("value got from Settings: " + value); 
        switch (value)
        {
            case 0:
                CurrentDifficulty = Difficulty.EASY;
                break;
            case 1:
                CurrentDifficulty = Difficulty.MEDIUM;
                break;
            case 2:
                CurrentDifficulty = Difficulty.HARD;
                break;
        }
    }
}