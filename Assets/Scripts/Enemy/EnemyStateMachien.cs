using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyStateMachien : MonoBehaviour,IDamageable
{
    public EnemyDataSO enemyData;
    public GameObject target;
    private float _lastAttackTime ;
    private EnemyState _currentState = EnemyState.Idle;
    private IDamageable _damageable;
    private int _currentHealth;
    private bool _targetInRange;
    private Animator _animator;
    private EnemySpawnManager _enemySpawnManager;
    private LayerMask _targetLayer;
    public void InitializeEnemy()
    {
        if (enemyData.visualPrefab != null)
        {
            Instantiate(enemyData.visualPrefab, transform.position, Quaternion.identity, transform);
            _animator = GetComponentInChildren<Animator>();
            _currentHealth = enemyData.health;
            _enemySpawnManager = EnemySpawnManager.Instance;
            GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
        }
    }

    private void GameManagerOnOnGameStateChanged(GameState obj)
    {
        if (obj == GameState.GameOver)
        {
            _currentState = EnemyState.TargetDead;
        }
    }

    private void Update()
    {
       if(target.transform != null)
            UpdateLookDirection(target.transform);
        switch (_currentState)
        {
            case EnemyState.Idle:
                IdleBehavior();
                break;

            case EnemyState.Chase:
                ChaseBehavior(target.transform);
                break;

            case EnemyState.Attack:
                AttackBehavior(target.transform);
                break;
            case EnemyState.Dead:
                DeathBehavior();
                break;
            case EnemyState.TargetDead:
                //Stop
            default:
                throw new ArgumentOutOfRangeException(nameof(EnemyState), _currentState, null);

          
        }
    }

    private void UpdateLookDirection(Transform targetTransform)
    {
        Vector2 directionToPlayer = (targetTransform.transform.position - transform.position).normalized;
        _animator.SetFloat(Horizontal, directionToPlayer.x);
        _animator.SetFloat(Vertical, directionToPlayer.y);

    }

    private void IdleBehavior()
    {
       StartCoroutine(ChangeStateCoolDown(EnemyState.Chase,1f));
    }
    public void ChaseBehavior(Transform target)
    {
        _animator.SetBool(Walk,true);
       Vector2 direction = (target.position - transform.position).normalized;
       transform.Translate(direction * (enemyData.movementSpeed * Time.deltaTime));
        
    }
    //bugged will be fixed later
    public void AttackBehavior(Transform target)
    {
        if (Time.time < enemyData.attackSpeed + _lastAttackTime )
            return;
        _lastAttackTime = Time.time;
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector2 position = (Vector2)target.position;
        if(enemyData.debug)
            DrawDebugIsometricCube(position, enemyData.hitBox.size, Color.red, 1f,angle);
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(position, enemyData.hitBox.size, angle,target.gameObject.layer);
        foreach (Collider2D collider in hitColliders)
        {
            _damageable ??= collider.GetComponent<IDamageable>();
            if (_damageable != null)
                _damageable.Damage(enemyData.damage);
        }
        _animator.SetBool(Walk,false);
        _animator.Play("Attack");
        if (!_targetInRange)
            StartCoroutine(ChangeStateCoolDown(EnemyState.Idle,0));
    }
 
    public void DeathBehavior()
    {
        GameObject coin = Instantiate(enemyData.coinPrefab, transform.position, Quaternion.identity);
        _enemySpawnManager.HandleEnemyDeath();
        coin.GetComponent<Coin>().value = enemyData.goldValue;
        _animator.SetTrigger(IsDead);
        Destroy(gameObject);
    }
    
    public void Damage(int damage)
    {
        _currentHealth -= damage;
        _animator.Play("GetHit");
        if (_currentHealth <= 0)
            _currentState = EnemyState.Dead;
        else
            _currentState = EnemyState.Idle;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _damageable = other.GetComponent<IDamageable>();
        if (_damageable != null && _currentState != EnemyState.Attack)
        {
            StartCoroutine(ChangeStateCoolDown(EnemyState.Attack,0.5f));
            _targetInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            _targetInRange = false;
    }
    private IEnumerator ChangeStateCoolDown(EnemyState state , float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        _currentState = state;
            
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }


    #region AnimationStates
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int GetHit = Animator.StringToHash("GetHit");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int IsDead = Animator.StringToHash("IsDead");
    #endregion

    #region EnemyHitBoxDebug

        void DrawDebugIsometricCube(Vector2 center, Vector2 size, Color color, float duration, float angle)
        {
            float halfWidth = size.x * 0.5f;
            float halfHeight = size.y * 0.5f;

      
            float radAngle = Mathf.Deg2Rad * angle;

       
            Vector2[] cubeVertices = new Vector2[4];

            cubeVertices[0] = center + new Vector2(-halfWidth, -halfHeight);
            cubeVertices[1] = center + new Vector2(halfWidth, -halfHeight);
            cubeVertices[2] = center + new Vector2(halfWidth, halfHeight);
            cubeVertices[3] = center + new Vector2(-halfWidth, halfHeight);

       
            Vector2[] rotatedVertices = new Vector2[4];
            for (int i = 0; i < 4; i++)
            {
                rotatedVertices[i] = RotatePoint(cubeVertices[i], center, radAngle);
            }

            // Draw the rotated cube edges
            Debug.DrawLine(rotatedVertices[0], rotatedVertices[1], color, duration);
            Debug.DrawLine(rotatedVertices[1], rotatedVertices[2], color, duration);
            Debug.DrawLine(rotatedVertices[2], rotatedVertices[3], color, duration);
            Debug.DrawLine(rotatedVertices[3], rotatedVertices[0], color, duration);
        }

    // Helper function to rotate a point around a center
        Vector2 RotatePoint(Vector2 point, Vector2 center, float angle)
        {
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);

            float x = cos * (point.x - center.x) - sin * (point.y - center.y) + center.x;
            float y = sin * (point.x - center.x) + cos * (point.y - center.y) + center.y;

            return new Vector2(x, y);
        }

    #endregion
}
public enum EnemyState
{
    Idle,
    Chase,
    Attack,
    Dead,
    TargetDead
}