using UnityEngine;
using Shiro.Weapons;

public class UIManager : MonoBehaviour
{
    #region Instance

    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("UIManager");
                    _instance = singletonObject.AddComponent<UIManager>();
                }
            }

            return _instance;
        }
    }

    #endregion
    
    public GameObject pickUpWeaponButton;
    
}
