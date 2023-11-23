using System;
using Shiro.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerCore : MonoBehaviour
{       
        public Weapons currentWeaponData;
        protected PlayerEventHandler PlayerEventHandler;
        protected Animator Animator;
        private int _health = 99 ;
        private string _playerName;
        private int _coins;
        

        protected virtual void Awake()
        { 
          Animator = GetComponent<Animator>(); 
          PlayerEventHandler = GetComponent<PlayerEventHandler>();
        }
        protected virtual void OnEnable()
        {
            PlayerEventHandler.OnWeaponChanged += PlayerCore_OnWeaponChange;
        }
        protected virtual void OnDisable()
        {
            PlayerEventHandler.OnWeaponChanged -= PlayerCore_OnWeaponChange;
        }  
        private void PlayerCore_OnWeaponChange(Weapons newWeaponData)
        {
            currentWeaponData = newWeaponData;
            var ranged = newWeaponData as RangedWeapons;
            if (ranged != null)
                ranged.projectilePrefab.GetComponent<ProjectileHandler>().damage = newWeaponData.WeaponDamage;
        }
        public void SaveGame()
        {
            PlayerStats playerStats = new PlayerStats();
            playerStats.health = _health;
            playerStats.name = _playerName;
            playerStats.coins = _coins;

            // Save the identifier of the current weapon
            if (currentWeaponData != null)
            {
                playerStats.currentWeaponId = currentWeaponData.name; // Assuming 'name' is the identifier
            }
            else
            {
                playerStats.currentWeaponId = null; // No current weapon
            }

            // Save the player stats
            SaveManager.Instance.SerializeJson(playerStats);
        }

}
