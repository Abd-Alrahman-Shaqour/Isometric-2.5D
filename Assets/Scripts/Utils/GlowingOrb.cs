using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class GlowingOrb : MonoBehaviour
{
    [SerializeField]private float animationDuration;

    [SerializeField] private Sprite curroptedOrb, goldenOrb;

    private SpriteRenderer _spriteRenderer;
    private EnemySpawnManager _enemySpawnManager;
    private GameManager _gameManager;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemySpawnManager = EnemySpawnManager.Instance;
        _gameManager = GameManager.Instance;
        _enemySpawnManager.OnLevelStateChanged += EnemySpawnManagerOnLevelStateChanged;
    }

  

    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(transform.position.y + 0.1f, animationDuration).SetLoops(-1,LoopType.Yoyo);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            if (_enemySpawnManager.levelState != LevelState.AllEnemiesDead)
            {
                _spriteRenderer.sprite = curroptedOrb;
                _enemySpawnManager.UpdateGameState(LevelState.SpawnEnemies);
            }
    } 
    private void OnDestroy()
    {
        _enemySpawnManager.OnLevelStateChanged -= EnemySpawnManagerOnLevelStateChanged;
    }
    private void EnemySpawnManagerOnLevelStateChanged(LevelState levelState)
    {
        if (levelState == LevelState.AllEnemiesDead)
        {
            _spriteRenderer.sprite = goldenOrb;
       
        }     _gameManager.SaveGame();
            
    }

}
