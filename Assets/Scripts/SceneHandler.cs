using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private Object persistentScene;
    [SerializeField] private string nextSceneName;

    private void Awake()
    {
        SceneManager.LoadSceneAsync(persistentScene.name, LoadSceneMode.Additive);
    }

    [ContextMenu("ChangeScene")]
    public void NextScene()
    {
        // Unload the currently added scene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        // Load the next scene
        SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
    }
}
