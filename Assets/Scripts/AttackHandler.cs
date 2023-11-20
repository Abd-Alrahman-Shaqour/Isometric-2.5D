using UnityEngine;
using UnityEngine.InputSystem;
using Shiro.Events;
using Shiro.Weapons;

public class AttackHandler : MonoBehaviour
{
    public Weapons data;

    #region Set In Inspector
        [SerializeField] private SpriteRenderer weaponSprite;
        [SerializeField] private InputActionReference attackInput;
    #endregion


    #region Private
        private PlayerEventHandler _playerEventHandler;
        private Animator _animator;
        private float _lastAttackTime;
    #endregion
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerEventHandler = GetComponent<PlayerEventHandler>();
    }
    private void OnEnable()
    {
        _playerEventHandler.OnAttack += HandleHitBox;
        _playerEventHandler.OnFinish += AttackFinished;
        _playerEventHandler.OnWeaponChanged += WeaponChnaged;
        attackInput.action.performed += Attack;
    }
    private void OnDisable()
    {
        _playerEventHandler.OnAttack -= HandleHitBox;
        _playerEventHandler.OnFinish -= AttackFinished;
        _playerEventHandler.OnWeaponChanged -= WeaponChnaged;
        attackInput.action.performed -= Attack;
    }
    
    private void Attack(InputAction.CallbackContext obj)
    {
        if (Time.time < data.AttackSpeed + _lastAttackTime)
            return;
        _lastAttackTime = Time.time;
        _animator.SetTrigger("Attack");
        data.Attack(transform);
    }
    private void WeaponChnaged()
    {
        weaponSprite.sprite = data.WeaponSprite;
    }
    private void HandleHitBox()
    {
        //DoSomething
    }
    private void AttackFinished()
    {
        //DoSomething
    }
    
 }
