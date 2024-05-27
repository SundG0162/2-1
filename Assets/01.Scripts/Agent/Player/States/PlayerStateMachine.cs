using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState {  get; private set; }
    private Dictionary<PlayerStateEnum, PlayerState> _stateDictionary;

    private Player _player;

    public PlayerStateMachine()
    {
        _stateDictionary = new Dictionary<PlayerStateEnum, PlayerState>();
    }

    public void Initialize(PlayerStateEnum state, Player player)
    {
        _player = player;
        CurrentState = _stateDictionary[state];
        CurrentState.Enter();
    }

    public void ChangeState(PlayerStateEnum newState) 
    {
        if (!_player.CanStateChangeable) return;
        CurrentState.Exit();
        CurrentState = _stateDictionary[newState];
        CurrentState.Enter();
    }

    public void AddState(PlayerStateEnum stateEnum, PlayerState state)
    {
        _stateDictionary.Add(stateEnum, state);
    }
}
