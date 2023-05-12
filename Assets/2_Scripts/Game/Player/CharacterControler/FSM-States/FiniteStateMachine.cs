using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    private IState _currentState;
    private Dictionary<PlayerStates, IState> _allStates = new Dictionary<PlayerStates, IState>();

    public void FakeUpdate()
    {
        _currentState.OnUpdate();
    }
    public void FakeFixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }
    public void ChangeState(PlayerStates state)
    {
        if (_currentState != null) _currentState.OnExit();
        _currentState = _allStates[state];
        _currentState.OnEnter();
    }

    public void AddState(PlayerStates key, IState value)
    {
        if (_allStates.ContainsKey(key)) _allStates.Add(key, value);
        else _allStates[key] = value;
    }
}
