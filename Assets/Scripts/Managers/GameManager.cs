using System;
using GameAnalyticsSDK;
using UnityEngine;

public class GameManager : SingletonPersistant<GameManager>
{
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    private SaveManager _saveManager;
    public PlayerCore PlayerCore { get; set;}

    private void Awake()
    {
        _saveManager = SaveManager.Instance;
    }

    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.GameMenu:
                break;
            case GameState.GamePlay:
                break;
            case GameState.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void LeveCompleted()
    {
        PlayerCore ??= FindObjectOfType<PlayerCore>();
        GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "CoinsCount", PlayerCore.playerStats.coins);
    }
    
    public void SaveGame()
    {
        PlayerCore ??= FindObjectOfType<PlayerCore>();

        if (PlayerCore != null)
            _saveManager.SerializeJson(PlayerCore.playerStats);
        else
            Debug.LogError("PlayerCoreNotFound");
    }

    public PlayerStats LoadGame()
    {
       return _saveManager.DeserializeJson();
    }
}

public enum GameState
{
    MainMenu,
    GameMenu,
    GamePlay,
    GameOver
}