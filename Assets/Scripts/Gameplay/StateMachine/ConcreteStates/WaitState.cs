using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : GameState
{
    #region Wait

    private float secondsUntillBite;
    private float biteTimer;
    private bool  biteCountdownStarted;

    #endregion

    #region Delay For Catch

    private float catchDelay;
    private float catchTimer;
    private bool  isBites;

    #endregion

    public WaitState(StateMachine stateMachine, FishingManager gameManager) : base(stateMachine, gameManager)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("WAIT");

        // State Initial
        gameManager.SetWaitState();

        // Generate seconds until fish bites
        secondsUntillBite = Random.Range(3, 10);

        // Reset delay variables
        catchDelay = 1f;
        isBites = false;

        Debug.Log($"Wait for fish! {secondsUntillBite} seconds");

        // Start countdown
        biteCountdownStarted = true;
        biteTimer = secondsUntillBite;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (isBites)
        {
            if (Input.touchCount > 0)
            {
                Debug.Log("CATCH!");
                stateMachine.ChangeState(gameManager.CatchState);
            }

            catchTimer -= Time.deltaTime;

            if (catchTimer <= 0)
            {
                Debug.Log("Fish lost!");
                stateMachine.ChangeState(gameManager.IdleState);
            }
        }

        if (biteCountdownStarted)
        {
            biteTimer -= Time.deltaTime;

            // Check if countdown has ended
            if (biteTimer <= 0)
            {
                // Notify the player that the countdown has ended
                // TODO: IN GAME EFFECT
                gameManager.PullTextTrigger();
                Debug.Log("PULL!");
                isBites = true;
                catchTimer = catchDelay;
                biteCountdownStarted = false;
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

    public override void FrameLateUpdate()
    {
        base.FrameLateUpdate();
    }
}
