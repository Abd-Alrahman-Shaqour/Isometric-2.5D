using System;
using UnityEngine;

public class EnemySpawnManager : Singleton<EnemySpawnManager>
{
    public LevelState levelState;
    public event Action<LevelState> OnLevelStateChanged;
    public void UpdateGameState(LevelState newState)
    {
        levelState = newState;
        switch (newState)
        {

            case LevelState.SpawnEnemies:
                HandleSpawnEnemies();
                break;
            case LevelState.AllEnemiesDead:
                HandleAllEnemiesDead();
                break;
            case LevelState.GameOver:
                HandleGameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnLevelStateChanged?.Invoke(newState);
    }

    private void HandleSpawnEnemies()
    {
        throw new NotImplementedException();
    }

    private void HandleAllEnemiesDead()
    {
        throw new NotImplementedException();
    }

    private void HandleGameOver()
    {
        throw new NotImplementedException();
    }
}

public enum LevelState
{
   
    SpawnEnemies,
    AllEnemiesDead,
    GameOver
}
