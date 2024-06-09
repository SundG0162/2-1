using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private Player _player;
    [SerializeField]
    private int _currentHealth;
    [SerializeField]
    private int _maxHealth = 100;

    public void Initialize(Player player)
    {
        _player = player;
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(int damage, Vector3 hitPoint, Vector3 normal, float knockBackPower)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0) 
        {
            _player.StateMachine.ChangeState(PlayerStateEnum.Dead);
        }
    }
}
