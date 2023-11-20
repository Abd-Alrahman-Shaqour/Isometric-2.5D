using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.Serialization;

namespace Shiro.Weapons
{
    
    public abstract class Weapons : ScriptableObject
    {
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public Sprite WeaponSprite { get; private set; }
        [field: SerializeField] public float WeaponDamage { get; private set; }
        public abstract void Attack(Transform attacker);
        
    }
}