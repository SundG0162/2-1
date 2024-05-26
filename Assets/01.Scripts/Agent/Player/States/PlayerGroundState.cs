using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGroundState : PlayerState
{
    protected PlayerGroundState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.OnJumpEvent += HandleOnJumpEvent;
    }

    private void HandleOnJumpEvent()
    {
        if(_player.MovementCompo.IsGround)
        {
            _stateMachine.ChangeState(PlayerStateEnum.JumpingUp);
        }
    }

    public override void Exit()
    {
        _player.PlayerInput.OnJumpEvent -= HandleOnJumpEvent;
        _player.MovementCompo.StopImmediately();
        base.Exit();
    }

    public override void UpdateState()
    {
        if(!_player.MovementCompo.IsGround)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }
}
