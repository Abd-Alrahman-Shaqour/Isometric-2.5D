
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class NextSceneCollider : MonoBehaviour
{
    SceneChangeManager _sceneChangeManager;
    [SerializeField] private SceneDefs sceneDefs;
    private GameManager _gameManager;

    private void Awake()
    {
        _sceneChangeManager = SceneChangeManager.Instance;
        _gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _gameManager.SaveGame();
             _sceneChangeManager.NextScene(sceneDefs);
        }
           
    }
}
