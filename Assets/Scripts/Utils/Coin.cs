using System;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Coin : MonoBehaviour
{
    public int value;
    private Tween _tween;
    void Start()
    {
        _tween = transform.DOLocalMoveY(transform.position.y + 0.1f, 1f).SetLoops(-1,LoopType.Yoyo);
        
    }
    private void OnDestroy()
    {
       _tween.Kill();
    }
}   
