using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Shiro.Weapons;

public class AttackHandler : MonoBehaviour
{
    public Weapons data;
    public Transform firePoint;
    public bool canAttack = true;
    [SerializeField] private InputActionReference attackInput;
    private AnimationEventHandler _animationEventHandler;
    private Animator _animator;
    private Vector2 _offSet;
    private float LastAttackTime;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animationEventHandler = GetComponent<AnimationEventHandler>();
    }
    
    private void OnEnable()
    {
        _animationEventHandler.OnAttack += HandleHitBox;
        _animationEventHandler.OnFinish += AttackFinished;
        attackInput.action.performed += Attack;
    }

    private void OnDisable()
    {
        _animationEventHandler.OnAttack -= HandleHitBox;
        _animationEventHandler.OnFinish -= AttackFinished;
        attackInput.action.performed -= Attack;
    }


    private void Attack(InputAction.CallbackContext obj)
    {
        if (Time.time < data.AttackSpeed + LastAttackTime)
            return;
        LastAttackTime = Time.time;
        _animator.SetTrigger("Attack");
        data.Attack(transform);
    }

    private void HandleHitBox()
    {
        //DoSomething
    }

    private void AttackFinished()
    {
        //DoSomething
    }

    
 }
