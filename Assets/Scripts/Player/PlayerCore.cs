using System;
using Shiro.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerCore : MonoBehaviour
{       
        [HideInInspector]
        public PlayerStats playerStats;
        [HideInInspector]
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
            PlayerEventHandler.OnWeaponChanged += WeaponChanged;
        }
        protected virtual void OnDisable()
        {
            PlayerEventHandler.OnWeaponChanged -= WeaponChanged;
        }   
        private void WeaponChanged(Weapons newWeapon)
        {
            currentWeaponData = newWeapon;
            var ranged = newWeapon as RangedWeapons;
            if (ranged != null)
                ranged.projectilePrefab.GetComponent<ProjectileHandler>().damage = newWeapon.WeaponDamage;
        }
        
}
