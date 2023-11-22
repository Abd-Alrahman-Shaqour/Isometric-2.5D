using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shiro.Weapons
{
    [CreateAssetMenu(fileName = "NewWeapons", menuName = "NewWeapons/MeleeWeapon", order = 0)]
    public class MeleeWeapon : Weapons
    {
        [field: Header("Melee Weapon Settings")]
        [field: SerializeField] public Rect HitBox { get; private set; }
        [field: SerializeField] public LayerMask targetLayer;

        private Vector2 _offset;

        public override void Attack(Transform attacker)
        {
            InputAction aimAction = AimActionReference.action;
            Debug.Log("MeleeAttack");
            MeleeAttack(attacker, LastAimDirection);
        }

        void MeleeAttack(Transform attacker, Vector2 aimDirection)
        {
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            var position = attacker.position;
            _offset.Set(position.x + (HitBox.size.x * angle), position.z);

            // Draw the isometric cube edges for debugging
            DrawDebugIsometricCube(_offset, HitBox.size, Color.red, 1f);

            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(_offset, HitBox.size, 0f, targetLayer);

            foreach (Collider2D collider in hitColliders)
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();
                if (damageable != null)
                    damageable.Damage(WeaponDamage);
            }
        }

        // DrawDebugIsometricCube method to visualize the isometric cube edges
        void DrawDebugIsometricCube(Vector2 center, Vector2 size, Color color, float duration)
        {
            float halfWidth = size.x * 0.5f;
            float halfHeight = size.y * 0.5f;

            Vector3[] cubeVertices = new Vector3[4];

            cubeVertices[0] = center + new Vector2(-halfWidth, -halfHeight);
            cubeVertices[1] = center + new Vector2(halfWidth, -halfHeight);
            cubeVertices[2] = center + new Vector2(halfWidth, halfHeight);
            cubeVertices[3] = center + new Vector2(-halfWidth, halfHeight);

            Debug.DrawLine(cubeVertices[0], cubeVertices[1], color, duration);
            Debug.DrawLine(cubeVertices[1], cubeVertices[2], color, duration);
            Debug.DrawLine(cubeVertices[2], cubeVertices[3], color, duration);
            Debug.DrawLine(cubeVertices[3], cubeVertices[0], color, duration);
        }
    }
}
