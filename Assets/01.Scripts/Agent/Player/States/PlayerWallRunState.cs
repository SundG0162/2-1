using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRunState : PlayerState
{
    public PlayerWallRunState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.OnJumpEvent += HandleOnJumpEvent;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    private void HandleOnJumpEvent()
    {
    }
}
