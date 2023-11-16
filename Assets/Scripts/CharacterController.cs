using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Animator _animator;
    private Rigidbody2D _rb;
    [SerializeField] private InputActionReference movementInput, attackInput, aimInput;
    private WeaponParent _weaponParent;
    [SerializeField] private Transform weaponPivot;
    private Vector2 scale;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        scale = weaponPivot.localScale;
    }

    private void FixedUpdate()
    {
        HandleInput();
    }
    
    private void HandleInput()
    {
        var movement = movementInput.action.ReadValue<Vector2>();
        movement.Normalize(); // Normalize to ensure consistent speed in all directions

        _rb.velocity = movement * (speed * Time.deltaTime);

        FlipWeapon();
        if (movementInput.action.IsPressed())
        {
            UpdateAnimation();
        }
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat("Horizontal", _rb.velocity.x);
        _animator.SetFloat("Vertical", _rb.velocity.y);
    }
    private void FlipWeapon()
    {
        Vector2 movement = _rb.velocity;

        // Check if moving upwards or upwards-left
        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            weaponPivot.localScale = new Vector3(1, 1, 1);
        }
        else if (Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
        {
            // Face left only when moving left
            weaponPivot.localScale = new Vector3(-1, 1, 1);
        }
        
    }
}
