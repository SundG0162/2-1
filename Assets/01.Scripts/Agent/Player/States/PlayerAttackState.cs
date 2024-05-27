using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int _currentComboCounter = 0;
    private float _lastAttackTime = 0;
    private float _comboWindow = 1f;
    private readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");

    public PlayerAttackState(PlayerStateMachine stateMachine, Player player, string animBoolname) : base(stateMachine, player, animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        bool comboCounterOver = _currentComboCounter > 2;
        bool comboWindowExhaust = Time.time > _lastAttackTime + _comboWindow;
        if (comboCounterOver || comboWindowExhaust)
            _currentComboCounter = 0;
        Debug.Log(_currentComboCounter);
        _player.AnimatorCompo.SetInteger(_comboCounterHash, _currentComboCounter);

        _player.MovementCompo.StopImmediately();

        _player.MovementCompo.ModifyFollowHeadCam(true);
    }

    public override void Exit()
    {
        _currentComboCounter++;
        _lastAttackTime = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
            _stateMachine.ChangeState(PlayerStateEnum.Idle);

        _player.MovementCompo.ModifyFollowHeadCam(false);
    }
}
