using UnityEngine;

namespace Shiro.Weapons
{
    [CreateAssetMenu(fileName = "NewWeapons", menuName = "NewWeapons/MeleeWeapon", order = 0)]
    public class MeleeWeapon : Weapons
    {
        [field: Header("Melee Weapon Settings")]
        [field: SerializeField] public Vector2 HitBoxSOffSet { get; private set; }
        [field: SerializeField] public Vector2 HitBoxSize { get; private set; }
        public override void Attack(Transform attacker)
        {
            Debug.Log("MeleeAttack");
        }
    }
}