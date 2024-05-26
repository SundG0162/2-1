using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideRunState : PlayerGroundState
{
    private Vector3 _movementDirection;
    public PlayerSideRunState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }


    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.OnMoveEvent += HandleOnMoveEvent;
        HandleOnMoveEvent(_player.PlayerInput.MoveInput);
    }

    private void HandleOnMoveEvent(Vector2 movement)
    {
        Debug.Log(movement);
        _movementDirection = new Vector3(movement.x, 0, movement.y) * _player.moveSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        float inputThreshold = 0.5f;
        if (_player.PlayerInput.MoveInput.magnitude < inputThreshold)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        if (!(Mathf.Abs(_player.PlayerInput.MoveInput.y) < inputThreshold))
        {
            _stateMachine.ChangeState(PlayerStateEnum.Run);
        }
        _player.MovementCompo.SetMovement(_movementDirection);
    }

    public override void Exit()
    {
        _player.PlayerInput.OnMoveEvent -= HandleOnMoveEvent;
        base.Exit();
    }
}
