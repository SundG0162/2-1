using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    private int _damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageable health))
        {
            health.ApplyDamage(_damage, Vector3.zero, Vector3.zero, 0);
        }
    }
}
