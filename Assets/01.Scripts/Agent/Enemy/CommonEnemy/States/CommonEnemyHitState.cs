using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemyHitState : EnemyState<CommonEnemyStateEnum>
{
    public CommonEnemyHitState(Enemy enemyBase, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if(_endTriggerCalled)
        {
            _stateMachine.ChangeState(CommonEnemyStateEnum.Idle);
        }
    }
}
