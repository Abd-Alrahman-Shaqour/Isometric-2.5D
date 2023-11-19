using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] private float projectilSpeed;
    [SerializeField] Ease ease;
    void Start()
    {
        transform.DOMove(transform.position + transform.up * projectilSpeed, 3f)
            .SetEase(ease)
            .OnComplete(() => Destroy(gameObject)); // Destroy the projectile when the tween is complete
    }
   
}
