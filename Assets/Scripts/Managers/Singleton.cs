
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    singletonObject.name = typeof(T).Name;
                    _instance = singletonObject.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}
public class SingletonPersistant<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                Scene activeScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("initial"));
                if (_instance == null)
                { 
                    GameObject singletonObject = new GameObject();
                    singletonObject.name = typeof(T).Name;
                    _instance = singletonObject.AddComponent<T>();
                }

                SceneManager.SetActiveScene(activeScene);
            }

            return _instance;
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
    public static void InitializeInstance()
    {
        // Accessing Instance property will initialize the instance
        T temp = Instance;
    }
}


