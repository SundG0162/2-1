using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpAttackState : PlayerState
{
    public PlayerJumpAttackState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.MovementCompo.ModifyFollowHeadCam(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_player.MovementCompo.IsGround)
        {
            _player.MovementCompo.StopImmediately();
        }
        if (_endTriggerCalled)
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    public override void Exit()
    {
        _player.MovementCompo.ModifyFollowHeadCam(false);
        base.Exit();
    }
}
