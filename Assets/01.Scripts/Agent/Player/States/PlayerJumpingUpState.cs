using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingUpState : PlayerAirState
{
    public PlayerJumpingUpState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.MovementCompo.SetYVelocity(_player.jumpPower);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_player.MovementCompo.Velocity.y < 0)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }
}
