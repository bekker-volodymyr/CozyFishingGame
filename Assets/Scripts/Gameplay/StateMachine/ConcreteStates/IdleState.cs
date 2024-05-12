using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IdleState : GameState
{
    public IdleState(StateMachine stateMachine, FishingManager gameManager) : base(stateMachine, gameManager)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("IDLE");

        gameManager.SetIdleState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            {
                stateMachine.ChangeState(gameManager.CastState);
            }
        }
    }

    public override void FrameLateUpdate()
    {
        base.FrameLateUpdate();
    }
}
