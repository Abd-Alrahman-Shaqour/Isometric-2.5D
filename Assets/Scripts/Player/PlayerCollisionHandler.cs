    using System;
    using UnityEngine;
    using Shiro.Weapons;

    public class PlayerCollisionHandler : MonoBehaviour
    {
        private PlayerEventHandler _eventHandler;

        private void Awake()
        {
            _eventHandler = GetComponentInParent<PlayerEventHandler>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("WeaponContainer"))
            {
                Weapons newWeaponData = other.GetComponent<WeaponContainer>().weaponPickUp;
                _eventHandler.newWeapon = newWeaponData;
                UIManager.Instance.pickUpWeaponButton.SetActive(true);
            }
        
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("WeaponContainer"))
            {
                _eventHandler.newWeapon = null;
                UIManager.Instance.pickUpWeaponButton.SetActive(false);
            }
            
        }

    
    }