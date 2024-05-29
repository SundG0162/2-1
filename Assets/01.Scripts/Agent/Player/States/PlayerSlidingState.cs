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
        _player.moveSpeed += 10f;
        _player.MovementCompo.ModifyLockCameraXAxisRotate(true);
        _player.MovementCompo.ModifyFollowHeadCam(true);
        _player.MovementCompo.ModifyForwardMovement(false);
        _player.PlayerInput.OnCrouchUpEvent += HandleOnCrouchUpEvent;
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
        _player.MovementCompo.ModifyFollowHeadCam(false);
        _player.MovementCompo.ModifyForwardMovement(true);
        _player.PlayerInput.OnCrouchUpEvent -= HandleOnCrouchUpEvent;
        _player.moveSpeed = _player.defaultMoveSpeed;
        _player.MovementCompo.SetMovement(Vector3.zero);
        base.Exit();
    }
}
