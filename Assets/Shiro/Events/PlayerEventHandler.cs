using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Shiro.Events
{
    public class PlayerEventHandler : MonoBehaviour
    {
        public event Action OnFinish;
        public event Action OnAttack;
        public  Action OnWeaponChanged;

        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
        private void OnAttackTrigger() => OnAttack?.Invoke();
        
    }
}