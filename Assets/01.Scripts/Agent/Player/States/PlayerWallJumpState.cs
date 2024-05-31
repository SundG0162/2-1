using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.MovementCompo.ModifyForwardMovement(false);
        _player.PlayerInput.OnMoveEvent += HandleOnMoveEvent;
        HandleOnMoveEvent(_player.PlayerInput.MoveInput);
    }

    private void HandleOnMoveEvent(Vector2 movement)
    {
        float movementThreshold = 0.05f;
        if(movement.magnitude > movementThreshold)
        {
            _player.MovementCompo.ModifyForwardMovement(true);
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }

    public override void Exit()
    {
        _player.PlayerInput.OnMoveEvent -= HandleOnMoveEvent;
        base.Exit();
    }
}
