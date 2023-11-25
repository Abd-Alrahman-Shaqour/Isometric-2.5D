using System;
using UnityEngine;

public class GameManager : SingletonPersistant<GameManager>
{
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    private SaveManager _saveManager;
    private PlayerCore _playerCore;

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
                HandleMainMenu();
                break;
            case GameState.GameMenu:
                HandleGameMenu();
                break;
            case GameState.GamePlay:
                HandleGamePlay();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    #region GameStates

    private void HandleMainMenu()
    {
        Debug.Log("MainMenu");
    }

    private void HandleGameMenu()
    {
        
    }

    private void HandleGamePlay()
    {
        Debug.Log("HandleGamePlay");
    }

    private void HandleGameOver()
    {
        throw new NotImplementedException();
    }

    #endregion
    
    public void SaveGame()
    {
        _playerCore ??= FindObjectOfType<PlayerCore>();

        if (_playerCore != null)
            _saveManager.SerializeJson(_playerCore.playerStats);
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