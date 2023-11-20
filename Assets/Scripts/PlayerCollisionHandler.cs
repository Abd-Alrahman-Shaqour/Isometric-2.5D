    using System;
    using UnityEngine;
    using Shiro.Events;
    using Shiro.Weapons;

    public class PlayerCollisionHandler : MonoBehaviour
    {
        #region Private
            private AttackHandler _attackHandler;
            private PlayerEventHandler _playerEventHandler;
            private CharacterController _characterController;
            private Weapons _weapons;
        #endregion

        private void Awake()
        {
            _playerEventHandler = FindObjectOfType<PlayerEventHandler>();
            _attackHandler = FindObjectOfType<AttackHandler>();
            _characterController = FindObjectOfType<CharacterController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("WeaponContainer"))
            {
                _weapons = other.GetComponent<WeaponContainer>().weaponPickUp;
                UIManager.Instance.weapons = _weapons;
                UIManager.Instance.pickUpWeaponButton.SetActive(true);
            }
        
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("WeaponContainer"))
            {
                _weapons = null;
                UIManager.Instance.weapons = _weapons;
                UIManager.Instance.pickUpWeaponButton.SetActive(false);
            }
            
        }

    
    }