using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using UnityEngine;


public enum PlayerStateEnum
{
    Idle,
    Run,
    JumpingUp,
    Fall,
    SideRun,
    Sliding,
    WallRun,
    Crouch,
    Dead
}
public class Player : Agent
{
    [Header("Setting Values")]  
    public float moveSpeed = 13f;
    public float jumpPower = 0.3f;
    public float crouchMoveSpeed = 6f;
    public float slidingSpeed = 30f;
    public float wallJumpPower = 0.32f;
    public float wallRunSpeed = 16f;

    [HideInInspector]
    public float defaultMoveSpeed, defaultJumpPower;

    [SerializeField]
    private InputReader _playerInput;
    public InputReader PlayerInput => _playerInput;

    public PlayerMovement MovementCompo { get; private set; }

    private PlayerStateMachine _stateMachine;
    public PlayerStateMachine StateMachine => _stateMachine;

    protected override void Awake()
    {
        base.Awake();

        defaultMoveSpeed = moveSpeed;
        defaultJumpPower = jumpPower;

        MovementCompo = GetComponent<PlayerMovement>();

        _stateMachine = new PlayerStateMachine();
        foreach(PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum)))
        {
            string enumname = stateEnum.ToString();
            try
            {
                Type t = Type.GetType($"Player{enumname}State");
                PlayerState state = Activator.CreateInstance(t, _stateMachine, this, enumname) as PlayerState;

                _stateMachine.AddState(stateEnum, state);
            }
            catch(Exception e) 
            {
                Debug.LogError($"{enumname} doesn't exist. :  {e.Message}");
            }
        }
    }

    private void Start()
    {
        _stateMachine.Initialize(PlayerStateEnum.Idle, this);
    }

    private void Update()
    {
        _stateMachine.CurrentState.UpdateState();
    }
}
