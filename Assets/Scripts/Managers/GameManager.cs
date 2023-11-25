using System;
using UnityEngine;

public class GameManager : SingletonPersistant<GameManager>
{
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    private SaveManager _saveManager;
    public PlayerCore PayerCore { get; set;}

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
    
    
    public void SaveGame()
    {
        PayerCore ??= FindObjectOfType<PlayerCore>();

        if (PayerCore != null)
            _saveManager.SerializeJson(PayerCore.playerStats);
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