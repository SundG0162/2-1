using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private Transform _firePos;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _player.PlayerInput.OnAttackEvent += HandleOnAttackEvent;
    }

    private void OnDestroy()
    {
        _player.PlayerInput.OnAttackEvent -= HandleOnAttackEvent;
    }

    private void HandleOnAttackEvent()
    {
    }
}
