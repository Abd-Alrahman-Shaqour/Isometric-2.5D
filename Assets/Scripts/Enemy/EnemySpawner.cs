using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyDataSO[] enemyDataArray;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private List<Transform> spawnTransforms = new List<Transform>();
    [SerializeField] private LayerMask playerLayer;

    private HashSet<Vector3> _occupiedPositions = new HashSet<Vector3>();
    
    public void InstantiateEnemies(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomSpawnPosition = GetRandomUnoccupiedPosition();
            GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity,transform);
            var enemy = newEnemy.GetComponent<EnemyStateMachien>();
            enemy.enemyData = enemyDataArray[Random.Range(0, enemyDataArray.Length)]; // Choose a random EnemyDataSO
            enemy.target = player;
            enemy.InitializeEnemy();

            _occupiedPositions.Add(randomSpawnPosition);
        }
    }

    private Vector3 GetRandomUnoccupiedPosition()
    {
        while (true)
        {
            Transform randomSpawnTransform = spawnTransforms[Random.Range(0, spawnTransforms.Count)];
            Vector3 randomSpawnPosition = randomSpawnTransform.position;

            if (_occupiedPositions.Add(randomSpawnPosition))
            {
                return randomSpawnPosition;
            }
        }
    }
}