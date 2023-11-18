using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Shiro.Weapons;
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Animator _animator;
    private Rigidbody2D _rb;
    [SerializeField] private InputActionReference movementInput, attackInput, aimInput;
    private WeaponParent _weaponParent;
    [SerializeField] private Transform weaponPivot;
    private AttackHandler _attackHandler;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _weaponParent = GetComponentInChildren<WeaponParent>();
        _attackHandler = GetComponent<AttackHandler>();
    }

    private void FixedUpdate()
    {
        HandleInput();
    }

    private void OnEnable()
    {
        attackInput.action.performed += PreformAttack;
    }

  
    private void OnDisable()
    {
        attackInput.action.performed -= PreformAttack;
    }  
    private void PreformAttack(InputAction.CallbackContext obj)
    { 
        _attackHandler.Attack();
    }


    private void HandleInput()
    {
        var movement = movementInput.action.ReadValue<Vector2>();
        movement.Normalize(); // Normalize to ensure consistent speed in all directions

        _rb.velocity = movement * (speed * Time.deltaTime);

        
        if (movementInput.action.IsPressed() )
        {
            UpdateAnimation();
        }
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat("Horizontal", _rb.velocity.x);
        _animator.SetFloat("Vertical", _rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WeaponContainer"))
        {
            _weaponParent.SetUpWeapon(other.GetComponent<WeaponContainer>().weaponPickUp);
        }
           
    }
}
