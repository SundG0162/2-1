using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAirState : PlayerState
{
    private Vector3 _movementDirection;

    public PlayerAirState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _player.PlayerInput.OnMoveEvent += HandleOnMoveEvent;
        _player.PlayerInput.OnAttackEvent += HandleOnAttackEvent;
        HandleOnMoveEvent(_player.PlayerInput.MoveInput);
    }

    private void HandleOnAttackEvent()
    {
        _stateMachine.ChangeState(PlayerStateEnum.JumpAttack);
    }

    private void HandleOnMoveEvent(Vector2 movement)
    {
        _movementDirection = new Vector3(movement.x, 0, movement.y);
    }

    public override void Exit()
    {
        _player.PlayerInput.OnMoveEvent -= HandleOnMoveEvent;
        _player.PlayerInput.OnAttackEvent -= HandleOnAttackEvent;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _player.MovementCompo.SetMovement(_movementDirection);
    }
}
