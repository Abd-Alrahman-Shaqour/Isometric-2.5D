using UnityEngine;
using TMPro;

public class TargetDummy:MonoBehaviour,IDamageable
{
    [SerializeField] private TextMeshPro textMeshPro;
    public void Damage(int damage)
    {
        textMeshPro.SetText(damage.ToString());
    }
}
