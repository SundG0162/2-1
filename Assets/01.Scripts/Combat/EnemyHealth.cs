using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeadEvent;

    private Enemy _owner;
    public Enemy Owner => _owner;
    private int _currentHealth;
    [SerializeField]
    private int _maxHealth;

    public void Initialize(Agent agent)
    {
        _owner = agent as Enemy;
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(int damage, Vector3 hitPoint, Vector3 normal, float knockBackPower)
    {
        if (knockBackPower > 0)
        {
            ApplyKnockback(normal * -knockBackPower);
        }

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _currentHealth);
        OnHitEvent?.Invoke();

        if (_currentHealth <= 0)
            OnDeadEvent?.Invoke();
    }

    private void ApplyKnockback(Vector3 force)
    {
        _owner.MovementCompo.GetKnockback(force);
    }
}
