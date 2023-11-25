using UnityEngine.SceneManagement;
public class SceneChangeManager : Singleton<SceneChangeManager>
{
    private SceneDefs _currentScene = SceneDefs.MENU;
    
    private void Awake()
    {
        SceneManager.LoadSceneAsync((int)_currentScene, LoadSceneMode.Additive);
    }
    public void NextScene(SceneDefs nextScene)
    {
        // Unload the currently added scene
        SceneManager.UnloadSceneAsync((int)_currentScene);
        // Load the next scene
        SceneManager.LoadSceneAsync((int)nextScene, LoadSceneMode.Additive);
        _currentScene = nextScene;
    }
}
public enum SceneDefs : byte {
    Intial = 0, // 0.Launch
    MENU = 1, // 1.Menu
    PlayerHub = 2, // 2.PlayerHub
    Arena = 3 // // 2.Arena
}

