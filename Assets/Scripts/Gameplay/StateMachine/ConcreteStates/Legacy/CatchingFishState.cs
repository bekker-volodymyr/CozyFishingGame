using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CatchingFishState : GameState
{
    private RectTransform fishTransform;
    private RectTransform rodTransform;

    private float successNewValue;

    private float increaseSpeed = 0.35f;

    private bool isTrembleUp = true;
    private float tremblinValue = 0.5f;

    public CatchingFishState(StateMachine stateMachine, FishingManager gameManager) : base(stateMachine, gameManager)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        gameManager.CatchingUI.SetActive(true);
        gameManager.SuccessIndicator.fillAmount = 0f;

        fishTransform = gameManager.FishIndicator.GetComponent<RectTransform>();
        rodTransform = gameManager.RodIndicator.GetComponent<RectTransform>();

        InitIndicators();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameLateUpdate()
    {
        base.FrameLateUpdate();

        gameManager.SuccessIndicator.fillAmount = successNewValue;

        FishTremblin();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (Mathf.Abs(Mathf.Abs(fishTransform.position.y) - Mathf.Abs(rodTransform.position.y)) < 30)
        {
            successNewValue += increaseSpeed * Time.deltaTime;
        }

        if(successNewValue >= 1f)
        {
            successNewValue = 1f;
            stateMachine.ChangeState(gameManager.IdleState);
        }


    }

    private void InitIndicators()
    {
        fishTransform.localPosition = new Vector3(0f, -336f, 0f);
        rodTransform.localPosition = new Vector3(0f, -336f, 0f);
    }

    private void FishTremblin()
    {
        Vector3 currentPos = fishTransform.localPosition;
        tremblinValue *= isTrembleUp ? 1 : -1;
        fishTransform.localPosition = new Vector3(currentPos.x, currentPos.y - tremblinValue, currentPos.z);
        isTrembleUp = !isTrembleUp;
    }
}
