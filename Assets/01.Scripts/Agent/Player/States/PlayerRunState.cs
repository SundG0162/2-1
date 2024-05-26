using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        float inputThreshold = 0.05f;
        if (_player.MovementCompo.Velocity.magnitude < inputThreshold)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}
