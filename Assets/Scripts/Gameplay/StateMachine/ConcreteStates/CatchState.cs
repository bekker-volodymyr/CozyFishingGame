using UnityEngine;

public class CatchState : GameState
{
    private float successNewValue;

    private float increaseSpeed = 0.35f;
    private float decreaseSpeed = 0.55f;

    private bool isColliding;

    public CatchState(StateMachine stateMachine, FishingManager gameManager) : base(stateMachine, gameManager)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        successNewValue = 0.1f;
        gameManager.SuccessIndicator.fillAmount = successNewValue;
        isColliding = true;

        gameManager.SetCatchState();

        gameManager.rodScript.CollisionEnter += SetCollidingTrue;
        gameManager.rodScript.CollisionExit += SetCollidingFalse;
    }

    public override void ExitState()
    {
        base.ExitState();

        gameManager.rodScript.CollisionEnter -= SetCollidingTrue;
        gameManager.rodScript.CollisionExit -= SetCollidingFalse;

        if(successNewValue == 1f)
        {
            gameManager.CatchFish();
        }
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
        if (isColliding)
        {
            successNewValue += increaseSpeed * Time.deltaTime;
        }
        else
        {
            successNewValue -= decreaseSpeed * Time.deltaTime;
        }

        if (successNewValue >= 1f)
        {
            successNewValue = 1f;
            stateMachine.ChangeState(gameManager.IdleState);
        }
        else if(successNewValue <= 0f) 
        {
            successNewValue = 0f;
            stateMachine.ChangeState(gameManager.IdleState);
        }
    }

    public override void FrameLateUpdate()
    {
        base.FrameLateUpdate();

        gameManager.SuccessIndicator.fillAmount = successNewValue;
    }

    private void SetCollidingTrue()
    {
        isColliding = true;
    }

    private void SetCollidingFalse()
    {
        isColliding = false;
    }
}
