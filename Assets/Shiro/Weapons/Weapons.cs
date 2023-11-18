using UnityEngine;

namespace Shiro.Weapons
{
    [CreateAssetMenu(fileName = "NewWeapons", menuName = "NewWeapons/Weapons", order = 0)]
    public class Weapons : ScriptableObject
    {
        [field: SerializeField] public float AttackDelay { get; private set; }
        [field: SerializeField] public Sprite WeaponSprite { get; private set; }
        [field: SerializeField] public float WeaponDamage { get; private set; }
    }
}