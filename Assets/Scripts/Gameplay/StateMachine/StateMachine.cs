using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public GameState CurrentState { get; set; }
    public void Initialize(GameState startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }
    public void ChangeState(GameState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}
