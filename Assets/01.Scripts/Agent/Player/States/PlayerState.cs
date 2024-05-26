using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine _stateMachine;
    protected Player _player;

    protected int _animBoolHash;
    protected bool _endTriggerCalled;

    public PlayerState(PlayerStateMachine stateMachine, Player player, string animBoolname)
    {
        _stateMachine = stateMachine;
        _player = player;
        _animBoolHash = Animator.StringToHash(animBoolname);
    }

    public virtual void Enter()
    {
        _player.AnimatorCompo.SetBool(_animBoolHash, true);
        _endTriggerCalled = false;
    }

    public virtual void UpdateState() { }

    public virtual void Exit()
    {
        _player.AnimatorCompo.SetBool(_animBoolHash, false);
    }

    public virtual void AnimationEndTrigger()
    {
        _endTriggerCalled = true;
    }
}
