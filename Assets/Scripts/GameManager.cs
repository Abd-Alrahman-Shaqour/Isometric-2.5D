using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SetUpManagers
    private static GameManager _instance;

        // Access the GameManager instance from anywhere
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    } 
    // References to other managers
     public UIManager uiManager;
     public AudioManager audioManager;
     public SaveManager saveManger;
     #endregion
     private void Awake()
     {
         if (_instance != null && _instance != this)
         {
             Destroy(this.gameObject);
         }
         else
         {
             _instance = this;
             DontDestroyOnLoad(this.gameObject);
         }
     
         // Initialize other managers
         InitializeManagers();
         
     }
     
     private void InitializeManagers()
     {
         if (uiManager == null)
         {
             uiManager = FindObjectOfType<UIManager>();
             if (uiManager == null)
             {
                 uiManager = gameObject.AddComponent<UIManager>();
             }
         }
     
         if (audioManager == null)
         {
             audioManager = FindObjectOfType<AudioManager>();
             if (audioManager == null)
             {
                 audioManager = gameObject.AddComponent<AudioManager>();
             }
         }
         if (saveManger == null)
         {
             saveManger = FindObjectOfType<SaveManager>();
             if (audioManager == null)
             {
                 saveManger = gameObject.AddComponent<SaveManager>();
             }
         }
     }
     
}