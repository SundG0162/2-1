using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    private bool _isSliding = false;
    public PlayerFallState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _isSliding = false;
        _player.PlayerInput.OnCrouchDownEvent += HandleOnCrouchDownEvent;
        _player.PlayerInput.OnCrouchUpEvent += HandleOnCrouchUpEvent;
    }

    private void HandleOnCrouchUpEvent()
    {
        _isSliding = false;
    }

    private void HandleOnCrouchDownEvent()
    {
        _isSliding = true;
    }

    public override void Exit()
    {
        _player.PlayerInput.OnCrouchDownEvent -= HandleOnCrouchDownEvent;
        _player.PlayerInput.OnCrouchUpEvent -= HandleOnCrouchUpEvent;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_player.MovementCompo.IsGround)
        {
            if (_isSliding)
            {
                _stateMachine.ChangeState(PlayerStateEnum.Sliding);
            }
            else
            {
                _stateMachine.ChangeState(PlayerStateEnum.Idle);
            }
        }
    }
}
