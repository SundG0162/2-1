using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.MovementCompo.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState(); 
        float inputThreshold = 0.05f;
        if (_player.PlayerInput.MoveInput.magnitude > inputThreshold)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Run);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
