using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastState : GameState
{
    private bool isGrow = true;
    private float newValue;
    private float speed = 0.75f;

    public CastState(StateMachine stateMachine, FishingManager gameManager) : base(stateMachine, gameManager)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        gameManager.SetCastState();
        newValue = 0;

        Debug.Log("CAST");
    }

    public override void ExitState()
    {
        base.ExitState();

        gameManager.GenerateFish(newValue);
        gameManager.PlaySound(SoundType.RodCast);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (gameManager.distanceIndicator.fillAmount >= 1) isGrow = false;
        else if (gameManager.distanceIndicator.fillAmount <= 0) isGrow = true;

        if (isGrow) newValue += speed * Time.deltaTime;
        else newValue -= speed * Time.deltaTime;

        if (/*Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended*/ Input.GetMouseButtonUp(0))
        {
            gameManager.SetAnimatorState(1);
            stateMachine.ChangeState(gameManager.WaitState);
        }
    }

    public override void FrameLateUpdate()
    {
        base.FrameLateUpdate();

        gameManager.distanceIndicator.fillAmount = newValue;
    }
}
