    using System;
    using UnityEngine;
    using Shiro.Weapons;

    public class PlayerCollisionHandler : MonoBehaviour
    {
        #region Private
            private Weapons _weapons;
            private PlayerEventHandler _playerEventHandler;
        #endregion

        private void Awake()
        {
            _playerEventHandler = FindObjectOfType<PlayerEventHandler>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("WeaponContainer"))
            {
                _weapons = other.GetComponent<WeaponContainer>().weaponPickUp;
                _playerEventHandler.weapons = _weapons;
                UIManager.Instance.pickUpWeaponButton.SetActive(true);
            }
        
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("WeaponContainer"))
            {
                _weapons = null;
                _playerEventHandler.weapons = _weapons;
                UIManager.Instance.pickUpWeaponButton.SetActive(false);
            }
            
        }

    
    }