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
        _player.MovementCompo.SetVelocity(_player.MovementCompo.Velocity + new Vector3(0, _player.jumpPower, 0));
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
