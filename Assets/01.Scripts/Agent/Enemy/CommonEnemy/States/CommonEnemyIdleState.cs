using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemyIdleState : EnemyState<CommonEnemyStateEnum>
{
    public CommonEnemyIdleState(Enemy enemyBase, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Collider target = _enemyBase.IsPlayerDetected();
        if (target == null) return;

        Vector3 direction = target.transform.position - _enemyBase.transform.position;
        direction.y = 0;
        if (!_enemyBase.IsObstacleDetected(direction.magnitude, direction.normalized))
        {
            _enemyBase.targetTrm = target.transform;
            _stateMachine.ChangeState(CommonEnemyStateEnum.Run);
        }
    }
}
