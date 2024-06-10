using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemyDeadState : EnemyState<CommonEnemyStateEnum>
{
    public CommonEnemyDeadState(Enemy enemyBase, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StageManager.Instance.DeregisterEnemy(_enemyBase);
        GameObject.Destroy(_enemyBase.gameObject);
    }
}
