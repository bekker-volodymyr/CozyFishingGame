using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaitForFishState : GameState
{
    private float secondsUntillBite;
    private float countdownTimer;
    private float catchDelay;
    private bool isBites;
    private float catchTimer;
    private bool countdownStarted;

    public WaitForFishState(StateMachine stateMachine, FishingManager gameManager) : base(stateMachine, gameManager)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        secondsUntillBite = Random.Range(3, 8);

        catchDelay = 1f;
        isBites = false;

        Debug.Log($"Wait for fish! {secondsUntillBite} seconds");

        // Start the countdown timer
        countdownStarted = true;
        countdownTimer = secondsUntillBite;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameLateUpdate()
    {
        base.FrameLateUpdate();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (isBites)
        {
            if (Input.touchCount > 0)
            {
                Debug.Log("CATCH!");
                stateMachine.ChangeState(gameManager.CatchingFishState);
            }

            catchTimer -= Time.deltaTime;

            if (catchTimer <= 0)
            {
                Debug.Log("Fish lost!");
                stateMachine.ChangeState(gameManager.IdleState);
            }

        }

        if (countdownStarted)
        {
            countdownTimer -= Time.deltaTime;

            // Check if countdown has ended
            if (countdownTimer <= 0)
            {
                // Notify the player that the countdown has ended
                Debug.Log("PULL!");
                isBites = true;
                catchTimer = catchDelay;
                countdownStarted = false;
            }
            else if (Input.touchCount > 0)
            {
                {
                    Debug.Log("Rod back!");
                    stateMachine.ChangeState(gameManager.IdleState);
                }
            }
        }
    }
}
