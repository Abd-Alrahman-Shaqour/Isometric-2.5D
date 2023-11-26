using System;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Coin : MonoBehaviour
{
    public int value;
    public Tween tween;
    void Start()
    {
        tween = transform.DOLocalMoveY(transform.position.y + 0.1f, 1f).SetLoops(-1,LoopType.Yoyo);
        
    }
    private void OnDestroy()
    {
      // tween.Kill();
    }
}   
