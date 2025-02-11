using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager Instance;

    [Header("Wave Settings")]
    public int totalWaves = 5; // Change this for balancing
    private int currentWave = 0;

    [Header("Enemy Tracking")]
    public int enemiesOnField = 0;
    public int enemiesDefeated = 0;

    [Header("Player Tracking")]
    public List<PlayerCharacter> playerParty = new List<PlayerCharacter>();

    [Header("Game State")]
    public bool isGameOver = false;
    public bool victoryAchieved = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        if (isGameOver) return;

        if (currentWave >= totalWaves)
        {
            CheckVictory();
            return;
        }

        currentWave++;
        enemiesOnField = SpawnEnemiesForWave(currentWave);
        enemiesDefeated = 0;
    }

    private int SpawnEnemiesForWave(int waveNumber)
    {
        // TODO: Implement enemy spawning logic based on wave number.
        return Mathf.Clamp(waveNumber * 2, 1, 10); // Example scaling
    }

    public void OnEnemyDefeated()
    {
        enemiesDefeated++;
        enemiesOnField--;

        if (enemiesOnField <= 0)
        {
            StartNextWave();
        }
    }

    public void OnPlayerDefeated(/*PlayerCharacter player*/)
    {
        //playerParty.Remove(player);
        CheckDefeat();
    }

    private void CheckVictory()
    {
        if (currentWave >= totalWaves && enemiesOnField == 0)
        {
            Debug.Log("Victory! Game Completed.");
            victoryAchieved = true;
            isGameOver = true;
            // TODO: Implement victory screen or progress to next stage
        }
    }

    private void CheckDefeat()
    {
        if (playerParty.Count == 0) // If all players are dead
        {
            Debug.Log("Game Over! All players are defeated.");
            isGameOver = true;
            // TODO: Implement game restart logic (roguelike style)
        }
    }
}
