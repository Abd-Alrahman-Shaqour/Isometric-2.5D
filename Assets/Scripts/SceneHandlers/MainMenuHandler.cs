using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    { 
        _gameManager = GameManager.Instance;
       _gameManager.UpdateGameState(GameState.MainMenu);   
    }

}
