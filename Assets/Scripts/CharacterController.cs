using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Shiro.Weapons;
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private InputActionReference movementInput, attackInput, aimInput;
    private Animator _animator;
    private Rigidbody2D _rb;
    private WeaponParent _weaponParent;
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
        HandleMovement();
         HandleAiming();
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
    
    private void HandleMovement()
    {
        var movement = movementInput.action.ReadValue<Vector2>();
        movement.Normalize(); // Normalize to ensure consistent speed in all directions

        _rb.velocity = movement * (speed * Time.deltaTime);

        
        if (!aimInput.action.IsPressed() && movementInput.action.IsPressed()  )
        {  
            UpdateAnimation(movement);
        }
    }
    private void HandleAiming()
    {
        var aim = aimInput.action.ReadValue<Vector2>();
        if (aimInput.action.IsPressed())
            UpdateAnimation(aim);
    }

    private void UpdateAnimation(Vector2 lookDirection)
    {
        _animator.SetFloat("Horizontal", lookDirection.x);
        _animator.SetFloat("Vertical", lookDirection.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WeaponContainer"))
        {
            _weaponParent.SetUpWeapon(other.GetComponent<WeaponContainer>().weaponPickUp);
        }
    }
}
