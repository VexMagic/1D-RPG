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
    public List<BaseCharacter> playerParty = new List<BaseCharacter>();

    [Header("Game State")]
    public bool isGameOver = false;
    public bool victoryAchieved = false;

    [Header("Enemy Spawning")]
    public GameObject[] enemyPrefabs; // Assign enemy prefabs in the Inspector

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
        Debug.Log($"Starting Wave {currentWave}");
        enemiesOnField = SpawnEnemiesForWave(currentWave);
        enemiesDefeated = 0;
    }

    private int SpawnEnemiesForWave(int waveNumber)
    {
        int enemyCount = Mathf.Clamp(waveNumber * 2, 1, 10);
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
            // Set enemy position or other properties
        }
        return enemyCount;
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

    public void OnPlayerDefeated(BaseCharacter player)
    {
        playerParty.Remove(player);
        if (playerParty.Count == 0) // Defeat if all players are dead
        {
            CheckDefeat();
        }
    }

    public void RevivePlayer(BaseCharacter player)
    {

        //if (player.isDead)
        //{
        //    player.isDead = false;
        //    playerParty.Add(player);
        //}
    }

    private void CheckVictory()
    {
        if (currentWave >= totalWaves && enemiesOnField == 0)
        {
            Debug.Log("Victory! Game Completed.");
            victoryAchieved = true;
            isGameOver = true;
            ShowVictoryScreen();
        }
    }

    private void CheckDefeat()
    {
        if (playerParty.Count == 0)
        {
            Debug.Log("Game Over! All players are defeated.");
            isGameOver = true;
            ShowGameOverScreen();
        }
    }

    private void ShowVictoryScreen()
    {
        // TODO: Implement victory screen or progress to next stage
        Debug.Log("Show Victory Screen");
    }

    private void ShowGameOverScreen()
    {
        // TODO: Implement game over screen or restart logic
        Debug.Log("Show Game Over Screen");
    }

    public void LoadCombatRoom()
    {
        // Reset wave and enemy tracking
        currentWave = 0;
        enemiesOnField = 0;
        enemiesDefeated = 0;
        StartNextWave();
    }

    public void LoadShopRoom()
    {
        // TODO: Implement shop logic
        Debug.Log("Entering Shop Room");
    }
}