using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IdleState : GameState
{
    private float initDelay = 0.3f;
    private float initDelayTimer;

    public IdleState(StateMachine stateMachine, FishingManager gameManager) : base(stateMachine, gameManager)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("IDLE");

        initDelayTimer = initDelay;

        gameManager.SetIdleState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (initDelayTimer > 0)
        {
            initDelayTimer -= Time.deltaTime;
            return;
        }

        if (/*(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)*/ Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                stateMachine.ChangeState(gameManager.CastState);
            }

            //if (!EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            //{
            //    stateMachine.ChangeState(gameManager.CastState);
            //}
        }
    }

    public override void FrameLateUpdate()
    {
        base.FrameLateUpdate();
    }
}
