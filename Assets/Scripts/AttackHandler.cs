using UnityEngine;
using UnityEngine.InputSystem;
using Shiro.Weapons;
using UnityEngine.Serialization;

public class AttackHandler : MonoBehaviour
{
    public Weapons data;

    #region Set In Inspector
        [SerializeField] private SpriteRenderer weaponSprite;
        [SerializeField] private  Transform weaponTransform;
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
        PlayerEventHandler.OnWeaponChanged += WeaponChanged;
        attackInput.action.performed += Attack;
    }
    private void OnDisable()
    {
        _playerEventHandler.OnAttack -= HandleHitBox;
        _playerEventHandler.OnFinish -= AttackFinished;
       PlayerEventHandler.OnWeaponChanged -= WeaponChanged;
        attackInput.action.performed -= Attack;
    }
    
    private void Attack(InputAction.CallbackContext obj)
    {
        if(data == null)
            return;
        if (Time.time < data.AttackSpeed + _lastAttackTime )
            return;
        _lastAttackTime = Time.time;
        _animator.SetTrigger("Attack");
        data.Attack(weaponTransform);
    }
    private void WeaponChanged(Weapons newWeaponData)
    {
        data = newWeaponData;
        weaponSprite.sprite = newWeaponData.WeaponSprite;
        var ranged = newWeaponData as RangedWeapons;
        if (ranged != null)
            ranged.projectilePrefab.GetComponent<ProjectileHandler>().damage = newWeaponData.WeaponDamage;
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
