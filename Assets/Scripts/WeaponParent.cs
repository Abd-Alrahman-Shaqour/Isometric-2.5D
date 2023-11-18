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
    public void SetUpWeapon(Weapons weapon)
    {
        attackHandler.attackDelay = weapon.AttackDelay;
        weaponSprit.sprite = weapon.WeaponSprite;
        attackHandler.weaponDamage = weapon.WeaponDamage;
        attackHandler.canAttack = true;
    }
    

}
