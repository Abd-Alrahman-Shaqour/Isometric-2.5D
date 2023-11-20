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
    [SerializeField]private BoxCollider2D weaponHitBox;
    public Action weaponChanged;
    public void SetUpWeapon(Weapons weapon)
    {
        weaponChanged?.Invoke();

    }

}
