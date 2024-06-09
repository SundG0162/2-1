using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CommonEnemyStateEnum
{
    Idle,
    Run,
    Fire,
    Hit,
    Dead
}

public class CommonEnemy : Enemy
{
    private EnemyStateMachine<CommonEnemyStateEnum> _stateMachine;
    public EnemyStateMachine<CommonEnemyStateEnum> StateMachine => _stateMachine;

    public CommonEnemyGun GunCompo { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        GunCompo = GetComponent<CommonEnemyGun>();
        _stateMachine = new EnemyStateMachine<CommonEnemyStateEnum>();

        foreach (CommonEnemyStateEnum stateEnum in Enum.GetValues(typeof(CommonEnemyStateEnum)))
        {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"CommonEnemy{typeName}State");
            try
            {
                var enemyState = Activator.CreateInstance(t, this, _stateMachine, typeName) as EnemyState<CommonEnemyStateEnum>;
                _stateMachine.AddState(stateEnum, enemyState);
            }
            catch (Exception e)
            {
                Debug.LogError($"CommonEnemy : no state [ {typeName} ]");
                Debug.LogError(e);
            }
        }
    }

    private void Start()
    {
        _stateMachine.Initialize(CommonEnemyStateEnum.Idle, this);
    }

    private void Update()
    {
        _stateMachine.CurrentState.UpdateState();
    }

    public override void AnimationEndTrigger()
    {
        
    }

    public void HandleOnHitEvent()
    {
        _stateMachine.ChangeState(CommonEnemyStateEnum.Hit, true);
    }

    public void HandleOnDeadEvent()
    {
        _stateMachine.ChangeState(CommonEnemyStateEnum.Dead, true);
    }
}
