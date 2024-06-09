using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemyFireState : EnemyState<CommonEnemyStateEnum>
{
    public CommonEnemyFireState(Enemy enemyBase, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Vector3 dir = (_enemyBase.targetTrm.position - _enemyBase.transform.position).normalized;
        _enemyBase.transform.rotation = Quaternion.LookRotation(dir);
        _enemyBase.transform.eulerAngles += new Vector3(0, 35, 0);

        (_enemyBase as CommonEnemy).GunCompo.Fire(dir);
        _enemyBase.MovementCompo.StopImmediately();
    }

    public override void Exit()
    {
        _enemyBase.lastAttackTime = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(CommonEnemyStateEnum.Run);
        }
    }
}
