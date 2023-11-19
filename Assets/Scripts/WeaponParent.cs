using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Shiro.Weapons;
using UnityEngine.Serialization;

public class WeaponParent : MonoBehaviour
{
    [SerializeField]private AttackHandler attackHandler;
    [SerializeField]private SpriteRenderer weaponSprit;
    [SerializeField] private BoxCollider2D weaponHitBox;
    public void SetUpWeapon(Weapons weapon)
    {
        attackHandler.attackDelay = weapon.AttackDelay;
        weaponSprit.sprite = weapon.WeaponSprite;
        attackHandler.weaponDamage = weapon.WeaponDamage;
        attackHandler.canAttack = true;
        if (!weapon.IsRanged)
        {   
            attackHandler.isRanged = weapon.IsRanged;
            weaponHitBox.enabled = true;
            weaponHitBox.size = weapon.HitBoxSize;
            weaponHitBox.offset = weapon.HitBoxSOffSet;
        }
        else
        {
            attackHandler.isRanged = weapon.IsRanged;
            attackHandler.ProjectilePrefab = weapon.ProjectilePrefab;
            attackHandler.firePoint = transform;
        }
        
    }

}
