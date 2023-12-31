using UnityEngine;
using UnityEngine.InputSystem;
using Shiro.Weapons;
using UnityEngine.Serialization;

public class AttackHandler : PlayerCore
{
    #region Set In Inspector
        [SerializeField] private SpriteRenderer weaponSprite;
        [SerializeField] private  Transform weaponTransform;
        [SerializeField] private InputActionReference attackInput;
    #endregion
    #region Private
        private float _lastAttackTime;
    #endregion
    
    protected  void OnEnable()
    {
        Debug.Log("atk override on enable");
        PlayerEventHandler.OnAttack += HandleHitBox;
        PlayerEventHandler.OnFinish += AttackFinished;
        PlayerEventHandler.OnWeaponChanged += AttackHandler_OnWeaponChange;
        attackInput.action.performed += Attack;
    }
    protected  void OnDisable()
    {
        PlayerEventHandler.OnAttack -= HandleHitBox;
        PlayerEventHandler.OnFinish -= AttackFinished;
        PlayerEventHandler.OnWeaponChanged -= AttackHandler_OnWeaponChange;
        attackInput.action.performed -= Attack;
    }
    
    private void Attack(InputAction.CallbackContext obj)
    {
        if(currentWeaponData == null)
            return;
        if (Time.time < currentWeaponData.AttackSpeed + _lastAttackTime )
            return;
        _lastAttackTime = Time.time;
        Animator.SetTrigger("Attack");
        audioManager.Play("PlayerAttack");
        currentWeaponData.Attack(weaponTransform);
    }
    private void AttackHandler_OnWeaponChange(Weapons newWeaponData)
    {
        weaponSprite.sprite = newWeaponData.WeaponSprite;
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
