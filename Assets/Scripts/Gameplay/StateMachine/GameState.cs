using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    protected FishingManager gameManager;
    protected StateMachine stateMachine;

    public GameState(StateMachine stateMachine, FishingManager gameManager)
    {
        this.stateMachine = stateMachine;
        this.gameManager = gameManager;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void FrameLateUpdate() { }
}
