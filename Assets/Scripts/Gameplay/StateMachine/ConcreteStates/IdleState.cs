using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GameState
{
    public IdleState(StateMachine stateMachine, FishingManager gameManager) : base(stateMachine, gameManager)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        gameManager.distanceIndicator.transform.parent.gameObject.SetActive(false);
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
            stateMachine.ChangeState(gameManager.CastingRodState);
        }
    }

    public override void FrameLateUpdate()
    {
        base.FrameLateUpdate();
    }
}
