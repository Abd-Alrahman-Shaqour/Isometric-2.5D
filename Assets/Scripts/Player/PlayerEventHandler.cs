﻿using System;
using Unity.VisualScripting;
using Shiro.Weapons;
using UnityEngine;

public class PlayerEventHandler : PlayerCore
{
   public event Action OnFinish;
   public event Action OnAttack;
   public event Action<Weapons> OnWeaponChanged;
   public Weapons newWeapon;
   private void AnimationFinishedTrigger() => OnFinish?.Invoke();
   private void OnAttackTrigger() => OnAttack?.Invoke();
   public void WeaponChanged() => OnWeaponChanged?.Invoke(newWeapon);
}