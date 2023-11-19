using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shiro.Weapons;

public class AttackHandler : MonoBehaviour
{
    public float attackDelay { get; set; }
    public float weaponDamage { get; set; }
    public bool isRanged { get; set; }
    public Transform firePoint;
    public bool canAttack = false;
    public GameObject ProjectilePrefab { get; set; }
    private AnimationEventHandler _animationEventHandler;
    private Animator _animator;
    private Vector2 _offSet;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animationEventHandler = GetComponent<AnimationEventHandler>();
    }

    private void OnEnable()
    {
        _animationEventHandler.OnAttack += HandleHitBox;
        _animationEventHandler.OnFinish += AttackFinished;
    }

    private void OnDisable()
    {
        _animationEventHandler.OnAttack -= HandleHitBox;
        _animationEventHandler.OnFinish -= AttackFinished;
    }


    public void Attack()
    {
        if(!canAttack)
            return;
        _animator.SetTrigger("Attack");
        if (isRanged)
            RangedAttack();
        canAttack = false;
        StartCoroutine(AttackDelay());
    }

    private void RangedAttack()
    {
        //ToFixInstantiatePos
        Instantiate(ProjectilePrefab, firePoint.position, firePoint.rotation);
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
    private void HandleHitBox()
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAA");
    }

    private void AttackFinished()
    {
        Debug.Log("Attack finished");
    }

    
 }
