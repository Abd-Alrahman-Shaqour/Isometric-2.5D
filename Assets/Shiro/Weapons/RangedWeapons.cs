using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace Shiro.Weapons
{
    [CreateAssetMenu(fileName = "NewWeapons", menuName = "NewWeapons/Ranged", order = 0)]
    public class RangedWeapons : Weapons
    {   
        [field: Header("Ranged Weapon Settings")]
        [field: SerializeField] public GameObject projectilePrefab { get; private set; }
        [field: SerializeField] public float projectilSpeed{ get; private set; }
        [field: SerializeField] public Sprite projectilSprite{ get; private set; }
        [field: SerializeField] public float projectilDuration{ get; private set; }
        [field: SerializeField] public Ease projectileEase{ get; private set; }
        [Header("Aim Action Reference")]
        [SerializeField]
        private InputActionReference aimActionReference;
        [SerializeField]
        private InputActionReference movementAimActionReference;
        private Vector2 _lastAimDirection;

        public InputActionReference AimActionReference
        {
            get
            {
                // If player is not moving, return the movementAimActionReference
                return aimActionReference.action.ReadValue<Vector2>() != Vector2.zero ? aimActionReference : movementAimActionReference;
            }
        }
        public override void Attack(Transform attacker)
        {
            InputAction aimAction = AimActionReference.action;
            FireProjectile(attacker,_lastAimDirection);
        }

        void FireProjectile(Transform attacker,Vector2 aimDirection)
        {
          
            float angle = Mathf.Atan2(aimDirection.y,aimDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            var position = attacker.position;
            GameObject projectile = Instantiate(projectilePrefab, position, rotation);
            projectile.GetComponent<SpriteRenderer>().sprite = projectilSprite;
            projectile.transform.DOMove(position + (Vector3)aimDirection * projectilSpeed, projectilDuration)
                .SetEase(projectileEase)
                .OnComplete(() => Destroy(projectile));
        }
        public void UpdateAimDirection()
        {
            // Use AimActionReference as needed
            InputAction aimAction = AimActionReference.action;

            // Read the aim direction
            Vector2 aimDirection = aimAction.ReadValue<Vector2>();

            // Save the last aim direction
            _lastAimDirection = aimDirection;
        }
    }
}