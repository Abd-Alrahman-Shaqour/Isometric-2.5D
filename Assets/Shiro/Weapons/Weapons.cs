using UnityEngine;
using UnityEngine.Serialization;

namespace Shiro.Weapons
{
    [CreateAssetMenu(fileName = "NewWeapons", menuName = "NewWeapons/Weapons", order = 0)]
    public class Weapons : ScriptableObject
    {
        [field: SerializeField] public float AttackDelay { get; private set; }
        [field: SerializeField] public Sprite WeaponSprite { get; private set; }
        [field: SerializeField] public float WeaponDamage { get; private set; }
        //if is a Melee weapon
        [field: SerializeField] public Vector2 HitBoxSize { get; private set; }
        [field: SerializeField] public Vector2 HitBoxSOffSet { get; private set; }
        //if is a Ranged weapon
        [field: SerializeField] public bool IsRanged { get; private set; } = false;
        [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
        
    }
}