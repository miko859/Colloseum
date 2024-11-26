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
        DontDestroyOnLoad(gameObject);
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
}