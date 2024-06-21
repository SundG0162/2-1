using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlidingState : PlayerGroundState
{
    public PlayerSlidingState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        float moveSpeedThreshold = 0.031f;
        _player.moveSpeed = _player.slidingSpeed;
        _player.MovementCompo.ModifyLockCameraXAxisRotate(true);
        _player.MovementCompo.LocalMoveMainCam(Vector3.up * 0.4f, 0.3f);
        _player.MovementCompo.ModifyForwardMovement(false);
        _player.PlayerInput.OnCrouchUpEvent += HandleOnCrouchUpEvent;
        if (_player.MovementCompo.Velocity.magnitude < moveSpeedThreshold)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Crouch);
        }
    }

    private void HandleOnCrouchUpEvent()
    {
        _player.MovementCompo.StopImmediately();
        _stateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_player.moveSpeed >= 0.05f)
            _player.moveSpeed -= 10f * Time.deltaTime;
        _player.MovementCompo.SetMovement(Vector3.forward);
        _player.MovementCompo.SetMovement(_player.MovementCompo.GetRotateVelocity() * _player.moveSpeed);
    }


    public override void Exit()
    {
        _player.MovementCompo.ModifyLockCameraXAxisRotate(false);
        _player.MovementCompo.LocalMoveMainCam(_player.MovementCompo.defaultMainCamLocalPos, 0.3f);
        _player.MovementCompo.ModifyForwardMovement(true);
        _player.PlayerInput.OnCrouchUpEvent -= HandleOnCrouchUpEvent;
        _player.moveSpeed = _player.defaultMoveSpeed;
        _player.MovementCompo.SetMovement(Vector3.zero);
        base.Exit();
    }
}
