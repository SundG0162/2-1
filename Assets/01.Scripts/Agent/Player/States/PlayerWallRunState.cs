using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRunState : PlayerState
{
    private Vector3 _wallNormalVec;
    private float _lastWallRunTime = 0;
    private float _lastWallRunThreshold = 0.3f;
    public PlayerWallRunState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.OnJumpEvent += HandleOnJumpEvent;
        _player.MovementCompo.ModifyForwardMovement(false);
        if (_lastWallRunTime + _lastWallRunThreshold > Time.time)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _player.PlayerInput.OnJumpEvent -= HandleOnJumpEvent;
        _player.MovementCompo.ModifyForwardMovement(true);
        _lastWallRunTime = Time.time;
        base.Exit();
    }

    private void HandleOnJumpEvent()
    {
        Vector3 wallJumpDir = _wallNormalVec + _player.transform.up;
        Debug.Log(wallJumpDir);
        float wallJumpForce = 0.35f;
        _player.MovementCompo.SetYVelocity(wallJumpDir.y * wallJumpForce);
        _player.MovementCompo.SetMovement(wallJumpDir * 50);
        _stateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_player.MovementCompo.IsGround)
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        if(_player.MovementCompo.CheckWall(out RaycastHit hit))
        {
            _wallNormalVec = hit.normal;
            Vector3 velocity = _player.MovementCompo.Velocity;
            _player.MovementCompo.SetYVelocity(velocity.y * 0.3f);
            Vector3 forward = Vector3.Cross(_wallNormalVec, _player.transform.up);
            if (Vector3.Dot(_player.transform.forward, forward) < 0)
            {
                forward = -forward;
            }
            float wallRunSpeed = 30f;
            _player.MovementCompo.SetMovement(forward * wallRunSpeed * Time.fixedDeltaTime);
        }
        else
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}
