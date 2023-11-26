using System;
using UnityEngine;

public class EnemySpawnManager : Singleton<EnemySpawnManager>
{
    public LevelState levelState;
    public static event Action<LevelState> OnLevelStateChanged;
    private int _numberOfEnemies;
    [SerializeField] private EnemySpawner enemySpawner;
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
    }

    public void UpdateEnemyState(LevelState newState,int enemiesToSpawn)
    {
        levelState = newState;
        switch (newState)
        {
            case LevelState.SpawnEnemies:
                HandleSpawnEnemies(enemiesToSpawn);
                break;
            case LevelState.AllEnemiesDead:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnLevelStateChanged?.Invoke(newState);
    }

   
    private void HandleSpawnEnemies(int enemiesToSpawn)
    {
        _audioManager.Play("EnemySpawn");
        _numberOfEnemies = enemiesToSpawn;
        enemySpawner.InstantiateEnemies(enemiesToSpawn);
    }
    public void HandleEnemyDeath()
    {
        _numberOfEnemies -= 1;
        if(levelState == LevelState.SpawnEnemies && _numberOfEnemies == 0)
            UpdateEnemyState(LevelState.AllEnemiesDead,0);
    }
    
 
}

public enum LevelState
{
    SpawnEnemies,
    AllEnemiesDead
}
