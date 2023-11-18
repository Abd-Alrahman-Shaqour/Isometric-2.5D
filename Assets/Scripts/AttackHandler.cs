using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [field: SerializeField] public float attackDelay { get; set; }
    [field: SerializeField] public float weaponDamage { get; set; }
    public bool canAttack = false;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        if(!canAttack)
            return;
        _animator.SetTrigger("Attack");
        canAttack = false;
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}
