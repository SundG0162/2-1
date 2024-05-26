using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
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
        float inputThreshold = 0.05f;
        if (_player.MovementCompo.Velocity.magnitude > inputThreshold)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Run);
        }
    }
}
