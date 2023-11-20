using Shiro.Events;
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

    private AttackHandler _attackHandler;
    private CharacterController _characterController;
    private PlayerEventHandler _playerEventHandler;
    public GameObject pickUpWeaponButton;
    public Weapons weapons;

    public void SwitchToNewWeapon()
    {
        CheckPlayerRefs();
        if (weapons == null)
            return;
        _attackHandler.data = weapons;
        _characterController.data = weapons;
        NotifyWeaponChanged();
    }

    private void CheckPlayerRefs()
    {
        if (_characterController == null)
            _characterController = FindObjectOfType<CharacterController>();
        if (_attackHandler == null)
            _attackHandler = FindObjectOfType<AttackHandler>();
        if (_playerEventHandler == null)
            _playerEventHandler = FindObjectOfType<PlayerEventHandler>();
    }

    private void NotifyWeaponChanged()
    {
        // Check if there are subscribers before invoking the event
        _playerEventHandler.OnWeaponChanged?.Invoke();
    }
}
