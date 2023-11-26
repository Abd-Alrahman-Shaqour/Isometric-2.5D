using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonPersistant<UIManager>
{
    public GameObject pickUpWeaponButton;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject gamePlayUI;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Scrollbar playerHealthUI;
    [SerializeField] private TMP_Text playerGold;
    private Canvas _canvas;
    private SceneChangeManager _sceneChangeManager;
    

    private void Awake()
    {

        _canvas = GameObject.FindGameObjectWithTag("UI").GetComponent<Canvas>();
        
        FindAndSetReferenceIfEmpty(ref mainMenuUI, "MainMenu");
        FindAndSetReferenceIfEmpty(ref gamePlayUI, "GamePlay");
        FindAndSetReferenceIfEmpty(ref gameMenu, "GameMenu");
        FindAndSetReferenceIfEmpty(ref gameOverUI, "GameOverUI");
        _sceneChangeManager = SceneChangeManager.Instance;
        GameManager.OnGameStateChanged += UpdateUiState;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= UpdateUiState;
    }

    public void UpdateUiState(GameState newState)
    {
        Debug.Log(newState);
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
    }

    #region GameStateHandlers

      private void HandleMainMenu()
        {
            mainMenuUI.SetActive(true);
            gameMenu.SetActive(false);
            gamePlayUI.SetActive(false);
            gameOverUI.SetActive(false);
        }
    
        private void HandleGameMenu()
        {
            mainMenuUI.SetActive(false);
            gameMenu.SetActive(true);
            gamePlayUI.SetActive(false);
            gameOverUI.SetActive(false);
        }
    
        private void HandleGamePlay()
        {
            mainMenuUI.SetActive(false);
            gameMenu.SetActive(false);
            gamePlayUI.SetActive(true);
            gameOverUI.SetActive(false);
        }
    
        private void HandleGameOver()
        {
            mainMenuUI.SetActive(false);
            gameMenu.SetActive(false);
            gamePlayUI.SetActive(false);
            gameOverUI.SetActive(true);
        }

    #endregion

  
    private void FindAndSetReferenceIfEmpty(ref GameObject reference, string name)
    {
        if (reference != null) return;
        Transform child = _canvas.transform.Find(name);
        if (child != null)
        {
            reference = child.gameObject;
        }
        else
        {
            Debug.LogError($"Could not find GameObject with name: {name} in UIManager.");
        }
    }

    #region UIFunctions
     public void StartGame()
    {
        _sceneChangeManager.NextScene(SceneDefs.PlayerHub);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

     public void UpdateHealthBar(int playerHealth)
     {
         playerHealthUI.size = playerHealth / 100f;
     }

     public void UpdateGold(int newGoldTotal)
     {
         playerGold.SetText(newGoldTotal.ToString());
     }

     #endregion
}
