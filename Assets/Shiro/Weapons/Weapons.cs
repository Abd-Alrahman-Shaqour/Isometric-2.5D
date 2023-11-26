using UnityEngine;
using UnityEngine.InputSystem;
namespace Shiro.Weapons
{
    
    public abstract class Weapons : ScriptableObject
    {
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public Sprite WeaponSprite { get; private set; }
        [field: SerializeField] public int WeaponDamage { get; private set; }
        [Header("Aim Action Reference")]
        [SerializeField]
        protected InputActionReference aimActionReference;
        [SerializeField]
        protected InputActionReference movementAimActionReference;
        protected Vector2 LastAimDirection;
        public abstract void Attack(Transform attacker);
        public InputActionReference AimActionReference
        {
            get
            {
                // If player is not moving, return the movementAimActionReference
                return aimActionReference.action.ReadValue<Vector2>() != Vector2.zero ? aimActionReference : movementAimActionReference;
            }
        }
        public void UpdateAimDirection()
        {
            // Use AimActionReference as needed
            InputAction aimAction = AimActionReference.action;

            // Read the aim direction
            Vector2 aimDirection = aimAction.ReadValue<Vector2>();

            // Save the last aim direction
            LastAimDirection = aimDirection;
        }
    }
}