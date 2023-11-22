using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Shiro.Weapons;
using UnityEngine.Serialization;

public class CharacterController : PlayerCore
{
    [SerializeField] private float movementSpeed = 50f;
    #region Private
        private Rigidbody2D _rb;
    #endregion
    //set in inspector
   [SerializeField] private InputActionReference movementInput,aimInput;
    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
    }
 
    private void FixedUpdate()
    {
        HandleMovement();
        HandleAiming();
    }
    
    private void HandleMovement()
    {
        var movement = movementInput.action.ReadValue<Vector2>();
        movement.Normalize(); // Normalize to ensure consistent speed in all directions

        _rb.velocity = movement * (movementSpeed * Time.deltaTime);

        
        if (!aimInput.action.IsPressed() && movementInput.action.IsPressed() )
        {  
            UpdateAnimation(movement);
            UpdateAim();
        }
    }
    private void HandleAiming()
    {
        var aim = aimInput.action.ReadValue<Vector2>();
        if (aimInput.action.IsPressed())
        {
            UpdateAnimation(aim);
            UpdateAim();
        }
            
    }

    private void UpdateAnimation(Vector2 lookDirection)
    {  
        Animator.SetFloat("Horizontal", lookDirection.x);
        Animator.SetFloat("Vertical", lookDirection.y);
    }

    //To Save The Last location The player Was looking at
    private void UpdateAim()
    {

        if (currentWeaponData == null)
            return;
            // Update the aim direction in the RangedWeapon scriptable object
            currentWeaponData.UpdateAimDirection();
        
    }
    
}
