using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForFishState : GameState
{
    private float secondsUntillCatch;
    private float countdownTimer;
    private bool countdownStarted;

    public WaitForFishState(StateMachine stateMachine, FishingManager gameManager) : base(stateMachine, gameManager)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        secondsUntillCatch = Random.Range(1, 8);

        Debug.Log($"Wait for fish! {secondsUntillCatch} seconds");

        // Start the countdown timer
        countdownStarted = true;
        countdownTimer = secondsUntillCatch;
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

        if (countdownStarted)
        {
            countdownTimer -= Time.deltaTime;

            // Check if countdown has ended
            if (countdownTimer <= 0)
            {
                // Notify the player that the countdown has ended
                Debug.Log("PULL!");

                // Check if the player touches the screen
                if (Input.touchCount > 0)
                {
                    Debug.Log("CATCH!");
                    stateMachine.ChangeState(gameManager.IdleState);
                }
                else
                {
                    Debug.Log("Fish lost!");
                    stateMachine.ChangeState(gameManager.IdleState);
                }
            }
            else if(Input.touchCount > 0)
            {
                Debug.Log("Rod back!");
                stateMachine.ChangeState(gameManager.IdleState);
            }
        }
    }
}
