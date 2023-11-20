using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Shiro.Weapons;
using Shiro.Events;
public class CharacterController : MonoBehaviour
{
    public Weapons data;
    [SerializeField] private float speed = 5f;
    //set in inspector
    [SerializeField] private InputActionReference movementInput, aimInput;

    #region Private
        private PlayerEventHandler _playerEventHandler;
        private Animator _animator;
        private Rigidbody2D _rb;
        private RangedWeapons rangedWeapon;
    #endregion

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _playerEventHandler = GetComponent<PlayerEventHandler>();
    }

    private void OnEnable()
    {
        _playerEventHandler.OnWeaponChanged += OnWeaponChanged;
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

        _rb.velocity = movement * (speed * Time.deltaTime);

        
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
        _animator.SetFloat("Horizontal", lookDirection.x);
        _animator.SetFloat("Vertical", lookDirection.y);
    }

    //To Save The Last location The player Was looking at  if it is a ranged weapon
    private void UpdateAim()
    {

        if (rangedWeapon == null)
            return;
            // Update the aim direction in the RangedWeapon scriptable object
            rangedWeapon.UpdateAimDirection();
        
    }

    private void OnWeaponChanged()
    {   
        // Check if the weapon is a RangedWeapon
        rangedWeapon = data as RangedWeapons;
    }

}
