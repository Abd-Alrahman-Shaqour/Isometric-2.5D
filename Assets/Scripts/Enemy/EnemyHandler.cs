 
using UnityEngine;

public class EnemyHandler : MonoBehaviour,IDamageable
{
        public int health = 100;
        public void Damage(int damage)
        {
                health -= damage;
        }
}
