using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyDataSO[] enemyDataArray;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject player;

    private void Start()
    {
        InstantiateEnemies();
    }

    private void InstantiateEnemies()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        foreach (var enemyData in enemyDataArray)
        {
            var enemy = newEnemy.GetComponent<EnemyStateMachien>();
            enemy.enemyData = enemyData;
            enemy.target = player;
            enemy.InitializeEnemy();
        }
    }
}
