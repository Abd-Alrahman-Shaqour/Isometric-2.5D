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
        protected AudioManager audioManager;
        private UIManager _uiManager;
        

        protected virtual void Awake()
        { 
          Animator = GetComponent<Animator>(); 
          PlayerEventHandler = GetComponent<PlayerEventHandler>();
          PlayerEventHandler.OnWeaponChanged += PlayerCore_OnWeaponChange;
          _gameManager = GameManager.Instance;
          _uiManager = UIManager.Instance;
          audioManager = AudioManager.Instance;
        }

        private void Start()
        {
            _gameManager.PlayerCore = this;
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
            audioManager.Play("CoinCollected");
            _uiManager.UpdateGold(playerStats.coins);
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
            _uiManager.UpdateGold(playerStats.coins);
            _uiManager.UpdateHealthBar(playerStats.health);
        }

        public void Damage(int damage)
        {
            playerStats.health -= damage;
            _uiManager.UpdateHealthBar(playerStats.health);
            if (playerStats.health <= 0)
            {
                playerStats.health = 100;
                _gameManager.SaveGame();
                _gameManager.UpdateGameState(GameState.GameOver);
                _gameManager.LeveCompleted();
                Destroy(gameObject);
            }
                
        }
        protected virtual void OnDestroy()
        {
            PlayerEventHandler.OnWeaponChanged -= PlayerCore_OnWeaponChange;
        }  

}
