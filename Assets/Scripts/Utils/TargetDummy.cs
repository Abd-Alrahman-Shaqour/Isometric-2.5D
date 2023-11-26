using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetDummy:MonoBehaviour,IDamageable
{
    [SerializeField] private TextMeshPro textMeshPro;
    public void Damage(int damage)
    {
        textMeshPro.SetText(damage.ToString());
        StartCoroutine(DeleteText());
    }

    IEnumerator DeleteText()
    {
        yield return new WaitForSeconds(2f);

        // Clear the text after waiting
        textMeshPro.SetText("");
    }
}
