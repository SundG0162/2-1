using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerGroundState
{
    private Vector3 _movementDirection;

    public PlayerCrouchState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.MovementCompo.LocalMoveMainCam(Vector3.up * 0.4f, 0.3f);
        _player.MovementCompo.ModifyForwardMovement(true);
        _player.PlayerInput.OnCrouchUpEvent += HandleOnCrouchUpEvent;
        _player.PlayerInput.OnMoveEvent += HandleOnMoveEvent;
        _player.moveSpeed = _player.crouchMoveSpeed;
    }

    private void HandleOnMoveEvent(Vector2 movement)
    {
        _movementDirection = new Vector3(movement.x, 0, movement.y);
    }

    private void HandleOnCrouchUpEvent()
    {
        _stateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _player.MovementCompo.SetMovement(_movementDirection);
    }

    public override void Exit()
    {
        _player.MovementCompo.LocalMoveMainCam(_player.MovementCompo.defaultMainCamLocalPos, 0.3f);
        _player.PlayerInput.OnCrouchUpEvent -= HandleOnCrouchUpEvent;
        _player.PlayerInput.OnMoveEvent -= HandleOnMoveEvent;
        _player.moveSpeed = _player.defaultMoveSpeed;

        base.Exit();
    }

}
