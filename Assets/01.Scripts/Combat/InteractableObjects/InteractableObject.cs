using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IDamageable
{
    public void ApplyDamage(int damage, Vector3 hitPoint, Vector3 normal, float knockBackPower)
    {
        Interact();
    }

    protected abstract void Interact();
}