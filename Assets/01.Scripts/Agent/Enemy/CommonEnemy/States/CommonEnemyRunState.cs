using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemyRunState : EnemyState<CommonEnemyStateEnum>
{
    public CommonEnemyRunState(Enemy enemyBase, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    private Vector3 _targetDestination;

    public override void Enter()
    {
        base.Enter();
        SetDestination(_enemyBase.targetTrm.position);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemyBase.MovementCompo.NavAgent.enabled)
        {
            _targetDestination = _enemyBase.MovementCompo.NavAgent.destination;
        }

        float distance = (_targetDestination - _enemyBase.targetTrm.position).magnitude;

        if (distance >= 0.5f)
        {
            SetDestination(_enemyBase.targetTrm.position);
        }

        if (Vector3.Distance(_enemyBase.transform.position, _enemyBase.targetTrm.position) <= _enemyBase.attackDistance)
        {
            _stateMachine.ChangeState(CommonEnemyStateEnum.Fire);
        }
    }

    private void SetDestination(Vector3 position)
    {
        _targetDestination = position;
        _enemyBase.MovementCompo.SetDestination(position);
    }

    

    public override void Exit()
    {
        base.Exit();
    }
}
