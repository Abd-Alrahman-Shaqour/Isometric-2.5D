using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Shiro.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinish;
        public event Action OnAttack;

        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
        private void OnAttackTrigger() => OnAttack?.Invoke();
        
    }
}