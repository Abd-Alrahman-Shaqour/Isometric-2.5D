using System;
using UnityEngine;
public class ProjectileHandler : MonoBehaviour
{
    public int damage;
    public LayerMask layer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.Damage(damage);
    }
    
}
