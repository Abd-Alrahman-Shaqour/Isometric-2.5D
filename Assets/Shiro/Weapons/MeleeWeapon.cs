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
        
        public override void Attack(Transform attacker)
        {
            InputAction aimAction = AimActionReference.action;
            Debug.Log("MeleeAttack");
            MeleeAtack(attacker, LastAimDirection);
        }
        private Vector2 _offSet;
        void MeleeAtack(Transform attacker,Vector2 aimDirection)
        {
            float angle = Mathf.Atan2(aimDirection.y,aimDirection.x) * Mathf.Rad2Deg;
            var position = attacker.position;
            _offSet.Set(position.x + (HitBox.size.x*angle),position.y);
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(_offSet, HitBox.size, 0f, targetLayer);

            foreach (Collider2D collider in hitColliders)
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();
                if (damageable != null)
                    damageable.Damage(WeaponDamage);
            }
        }
    }
}