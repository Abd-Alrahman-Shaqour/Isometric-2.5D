using System;
using System.Collections.Generic;
using Shiro.Weapons;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerCore : MonoBehaviour,IDamageable
{       
        public Weapons currentWeaponData;
        [SerializeField] private List<Weapons> allWeapons;
        public PlayerStats playerStats = new PlayerStats();
        protected PlayerEventHandler PlayerEventHandler;
        protected Animator Animator;
        protected GameManager _gameManager;

        protected virtual void Awake()
        { 
          Animator = GetComponent<Animator>(); 
          PlayerEventHandler = GetComponent<PlayerEventHandler>();
          PlayerEventHandler.OnWeaponChanged += PlayerCore_OnWeaponChange;
          _gameManager = GameManager.Instance;
        }

        private void Start()
        {
            LoadPlayerStats();
        }
      
        private void PlayerCore_OnWeaponChange(Weapons newWeaponData)
        {
            currentWeaponData = newWeaponData;
            playerStats.currentWeaponId = currentWeaponData.name;
            var ranged = newWeaponData as RangedWeapons;
            if (ranged != null)
                ranged.projectilePrefab.GetComponent<ProjectileHandler>().damage = newWeaponData.WeaponDamage;
        }

        public void CollectCoins(int amount)
        {
            playerStats.coins += amount;
        }
        private void LoadPlayerStats()
        {
            playerStats = _gameManager.LoadGame();
            Weapons newWeapon = allWeapons.Find(w => w.name == playerStats.currentWeaponId);

            if (newWeapon != null)
            {
                PlayerEventHandler.newWeapon = newWeapon;
                PlayerEventHandler.WeaponChanged();
            }
        }

        public void Damage(int damage)
        {
            playerStats.health -= damage;
            if ( playerStats.health  <= 0)
                _gameManager.UpdateGameState(GameState.GameOver);
        }
        protected virtual void OnDestroy()
        {
            PlayerEventHandler.OnWeaponChanged -= PlayerCore_OnWeaponChange;
        }  

}
