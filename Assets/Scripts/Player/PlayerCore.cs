using System;
using Shiro.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerCore : MonoBehaviour
{       
        public PlayerStats playerStats = new PlayerStats();
       
        public Weapons currentWeaponData;
        protected PlayerEventHandler PlayerEventHandler;
        protected Animator Animator;

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
            playerStats.currentWeapon = newWeaponData;
            currentWeaponData = newWeaponData;
            var ranged = newWeaponData as RangedWeapons;
            if (ranged != null)
                ranged.projectilePrefab.GetComponent<ProjectileHandler>().damage = newWeaponData.WeaponDamage;
        }

}
