using System;
using System.Collections;
using UnityEngine;

public class EnemyStateMachien : MonoBehaviour,IDamageable
{
    public EnemyDataSO enemyData;
    public GameObject target;
    private float _lastAttackTime ;
    private EnemyState _currentState = EnemyState.Chase;
    private IDamageable _damageable;
    private int _currentHealth;
    private bool _targetInRange;
 
    public void InitializeEnemy()
    {
        if (enemyData.visualPrefab != null)
        {
            Instantiate(enemyData.visualPrefab, transform.position, Quaternion.identity, transform);
            _currentHealth = enemyData.health;
        }
    }
    private void Update()
    {
        
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
            default:
                throw new ArgumentOutOfRangeException(nameof(EnemyState), _currentState, null);

          
        }
    }
    
    private void IdleBehavior()
    {
       //donothing
    }
    public void ChaseBehavior(Transform target)
    {
        
       Vector2 direction = (target.position - transform.position).normalized;
       transform.Translate(direction * (enemyData.movementSpeed * Time.deltaTime));
        
    }
    
    public void AttackBehavior(Transform target)
    {
        if (Time.time < enemyData.attackSpeed + _lastAttackTime )
            return;
        _lastAttackTime = Time.time;
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector2 position = (Vector2)target.position;
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(position, enemyData.hitBox.size, angle,target.gameObject.layer);

        foreach (Collider2D collider in hitColliders)
        {
            _damageable ??= collider.GetComponent<IDamageable>();
            if (_damageable != null)
                _damageable.Damage(enemyData.damage);
        }
        if (!_targetInRange)
            StartCoroutine(ChangeStateCoolDown(EnemyState.Chase));
    }
    public void DeathBehavior()
    {
        Destroy(gameObject);
        GameObject coin = Instantiate(enemyData.coinPrefab, transform.position, Quaternion.identity);
        coin.GetComponent<Coin>().value = enemyData.goldValue;
    }
    public void Damage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log("EnemyHealth" + _currentHealth);
        if (_currentHealth <= 0)
            _currentState = EnemyState.Dead;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _damageable = other.GetComponent<IDamageable>();
        if (_damageable != null && _currentState != EnemyState.Attack)
        {
            _currentState = EnemyState.Attack;
            _targetInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            _targetInRange = false;
    }
    private IEnumerator ChangeStateCoolDown(EnemyState state)
    {
        _currentState = EnemyState.Idle;
        yield return new WaitForSeconds(1f);
        _currentState = state;
    }
}
public enum EnemyState
{
    Idle,
    Chase,
    Attack,
    Dead,
}