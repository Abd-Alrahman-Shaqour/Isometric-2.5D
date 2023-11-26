    using UnityEngine;
    using Shiro.Weapons;
    using UnityEngine.UI;

    public class PlayerCollisionHandler : MonoBehaviour
    {
        private PlayerEventHandler _eventHandler;
        private UIManager _uiManager;
        private PlayerCore _playerCore;

        private  void Awake()
        {
            _eventHandler = GetComponentInParent<PlayerEventHandler>();
            _uiManager = UIManager.Instance;
            _playerCore = GetComponentInParent<PlayerCore>();
        }

        private void Start()
        {
            _uiManager.pickUpWeaponButton.GetComponent<Button>().onClick.AddListener(_eventHandler.WeaponChanged);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("WeaponContainer"))
            {
                Weapons newWeaponData = other.GetComponent<WeaponContainer>().weaponPickUp;
                _eventHandler.newWeapon = newWeaponData;
                _uiManager.pickUpWeaponButton.SetActive(true);
            }

            if (other.CompareTag("Coin"))
            {
                _playerCore.CollectCoins(other.GetComponent<Coin>().value);
                Destroy(other.gameObject);
            }
        
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("WeaponContainer"))
            {
                _eventHandler.newWeapon = null;
                _uiManager.pickUpWeaponButton.SetActive(false);
            }
            
        }

    
    }