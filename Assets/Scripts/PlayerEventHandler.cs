using System;
using Unity.VisualScripting;
using Shiro.Weapons;
using UnityEngine;

public class PlayerEventHandler : MonoBehaviour
{
   public event Action OnFinish;
   public event Action OnAttack;
   public static event Action<Weapons> OnWeaponChanged;
   public Weapons weapons;
   private void AnimationFinishedTrigger() => OnFinish?.Invoke();
   private void OnAttackTrigger() => OnAttack?.Invoke();
   public void WeaponChanged() => OnWeaponChanged?.Invoke(weapons);
}