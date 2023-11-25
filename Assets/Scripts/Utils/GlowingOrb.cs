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
    
    [SerializeField]private GameObject doorExit;
    [SerializeField] private AudioSource audioSource;
    private SpriteRenderer _spriteRenderer;
    private EnemySpawnManager _enemySpawnManager;
    private GameManager _gameManager;
    private AudioManager _audioManager;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemySpawnManager = EnemySpawnManager.Instance;
        _gameManager = GameManager.Instance;
        _audioManager = AudioManager.Instance;
        EnemySpawnManager.OnLevelStateChanged += EnemySpawnManagerOnLevelStateChanged;
        GameManager.OnGameStateChanged += GameManagerOnLevelStateChanged;
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
                _enemySpawnManager.UpdateEnemyState(LevelState.SpawnEnemies,5);
                GetComponent<BoxCollider2D>().enabled = false;
                doorExit.SetActive(false);
            }
    } 
    private void OnDestroy()
    {
        EnemySpawnManager.OnLevelStateChanged -= EnemySpawnManagerOnLevelStateChanged;
    }
    private void GameManagerOnLevelStateChanged(GameState obj)
    {
        if(obj == GameState.GameOver)
            audioSource.Stop();
    }
    private void EnemySpawnManagerOnLevelStateChanged(LevelState levelState)
    {
        if (levelState == LevelState.AllEnemiesDead)
        {
            _spriteRenderer.sprite = goldenOrb;
            _audioManager.Play("DoorOpen");
            doorExit.SetActive(true);
            _gameManager.SaveGame();
        }     
    }

}
