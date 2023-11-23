using System;
using UnityEngine;
using Shiro.Weapons;

public class UIManager : Singleton<UIManager>
{
    public GameObject pickUpWeaponButton;
    public static event Action<UiState> OnUIStateChanged; 
    public UiState State;

    public void UpdateUiState(UiState newState)
    {
        State = newState;
        switch (newState)
        {
            case UiState.MainMenu:
                HandleMainMenu();
                break;
            case UiState.GameMenu:
                HandleGameMenu();
                break;
            case UiState.GamePlay:
                HandleGamePlay();
                break;
            case UiState.GameOver:
                HandleGameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnUIStateChanged?.Invoke(newState);
    }

    private void HandleMainMenu()
    {
        throw new NotImplementedException();
    }

    private void HandleGameMenu()
    {
        throw new NotImplementedException();
    }

    private void HandleGamePlay()
    {
        throw new NotImplementedException();
    }

    private void HandleGameOver()
    {
        throw new NotImplementedException();
    }
}

public enum UiState
{
    MainMenu,
    GameMenu,
    GamePlay,
    GameOver
}
