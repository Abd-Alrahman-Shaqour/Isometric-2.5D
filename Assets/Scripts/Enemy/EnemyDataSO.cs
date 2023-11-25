using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "DefaultEnemy", order = 0)]
public class EnemyDataSO : ScriptableObject
{
    [Header("Attributes")]
    public float movementSpeed = 5f;
    public int health = 100;
    public int damage = 10;
    public float attackSpeed = 1f;
    public Rect hitBox;
    public int goldValue;

    [Header("References")]
    public GameObject visualPrefab; // Prefab to visualize the enemy

    public GameObject coinPrefab; // Prefab of the coin dropped but the enemy
}
