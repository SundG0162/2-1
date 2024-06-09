using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : Agent
{
    [Header("Common Setting")]
    public float moveSpeed;
    public float battleTime;

    public EnemyMovement MovementCompo { get; protected set; }
    public EnemyHealth HealthCompo {get; protected set; }

    protected float _defaultMoveSpeed;

    [SerializeField]
    protected LayerMask _whatIsPlayer;
    [SerializeField] 
    protected LayerMask _whatIsObstacle;

    [Header("Attack Setting")]
    public float runAwayDistance;
    public float attackDistance;
    public float attackCooldown;
    public int maxHealth = 10;
    [SerializeField]
    protected int _maxCheckEnemy = 1;
    [HideInInspector]
    public float lastAttackTime;

    [HideInInspector]
    public Transform targetTrm;
    [HideInInspector]
    public CapsuleCollider capsuleCollider;

    protected Collider[] _enemyCheckColliders;

    protected override void Awake()
    {
        base.Awake();

        _defaultMoveSpeed = moveSpeed;

        _enemyCheckColliders = new Collider[_maxCheckEnemy];
        MovementCompo = GetComponent<EnemyMovement>();
        HealthCompo = GetComponent<EnemyHealth>();
        HealthCompo.Initialize(this);
        MovementCompo.Initialize(this);
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public virtual Collider IsPlayerDetected()
    {
        int cnt = Physics.OverlapSphereNonAlloc(transform.position, runAwayDistance, _enemyCheckColliders, _whatIsPlayer);

        return cnt >= 1 ? _enemyCheckColliders[0] : null;
    }

    public virtual bool IsObstacleDetected(float distance, Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, distance, _whatIsObstacle);
    }

    public abstract void AnimationEndTrigger();
}
