using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;


public enum PlayerStateEnum
{
    Idle,
    Run,
    Jump,
    Fall,
    Attack
}
public class Player : Agent
{
    [Header("Setting Values")]
    public float moveSpeed = 10f;
    public float jumpPower = 6f;

    [SerializeField]
    private InputReader _playerInput;
    public InputReader PlayerInput => _playerInput;

    public PlayerMovement MovementCompo { get; private set; }

    private PlayerStateMachine _stateMachine;

    protected override void Awake()
    {
        base.Awake();

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

    #region InputEventHandles
    private void OnEnable()
    {
        PlayerInput.OnMoveEvent += HandleOnMoveEvent;
        PlayerInput.OnJumpEvent += HandleOnJumpEvent;
        PlayerInput.OnAttackEvent += HandleOnAttackEvent;
    }

    private void OnDisable()
    {
        PlayerInput.OnMoveEvent -= HandleOnMoveEvent;
        PlayerInput.OnJumpEvent -= HandleOnJumpEvent;
        PlayerInput.OnAttackEvent -= HandleOnAttackEvent;
    }


    private void HandleOnMoveEvent(Vector2 movement)
    {
        MovementCompo.SetMovement(new Vector3(movement.x, 0, movement.y) * moveSpeed);
    }

    private void HandleOnJumpEvent()
    {
    }

    private void HandleOnAttackEvent()
    {
    }




    #endregion
}
