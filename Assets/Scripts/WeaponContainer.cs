using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shiro.Weapons;
using DG.Tweening;

public class WeaponContainer : MonoBehaviour
{
    public Weapons weaponPickUp;
    [SerializeField]private float animationDuration;
    
    
    // Start is called before the first frame update
    void Start()
    {   
        if(weaponPickUp != null)
        GetComponent<SpriteRenderer>().sprite = weaponPickUp.WeaponSprite;
        ContainerAnim();
    }

    private void ContainerAnim()
    {
        transform.DOLocalMoveY(transform.position.y + 0.1f, animationDuration).SetLoops(-1,LoopType.Yoyo);
    }

}
