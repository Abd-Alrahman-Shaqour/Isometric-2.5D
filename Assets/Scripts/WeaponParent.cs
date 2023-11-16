using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 aimPosition { get; set; }

    // private void Update()
    // {
    //     var direction = (aimPosition - (Vector2)transform.position).normalized;
    //     transform.right = direction;
    // }
}
