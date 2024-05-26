using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_player.MovementCompo.IsGround)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}
