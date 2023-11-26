using GameAnalyticsSDK;
using UnityEngine;

public class HubAreaHandler : MonoBehaviour
{
    private GameManager _gameManager;
    void Start()
    { 
        _gameManager = GameManager.Instance;
        _gameManager.UpdateGameState(GameState.GamePlay);
        GameAnalytics.NewProgressionEvent (GAProgressionStatus.Start, "PlayerHub");
    }
}
