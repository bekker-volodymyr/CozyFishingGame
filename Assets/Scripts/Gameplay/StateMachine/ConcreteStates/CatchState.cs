using UnityEngine;

public class CatchState : GameState
{
    private float successNewValue;
    private float startValue = 0.2f;

    private float increaseSpeed = 0.25f;
    private float decreaseSpeed = 0.3f;

    private bool isColliding;

    private float initDelay = 0.5f;
    private float initDelayTimer;

    public CatchState(StateMachine stateMachine, FishingManager gameManager) : base(stateMachine, gameManager)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        successNewValue = startValue;
        gameManager.SuccessIndicator.fillAmount = successNewValue;
        isColliding = true;

        initDelayTimer = initDelay;

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
            gameManager.TextTrigger(TextType.Catch);
            gameManager.PlaySound(SoundType.SuccessCatch);
            gameManager.CatchFish();
        }
        else
        {
            gameManager.TextTrigger(TextType.Lost);
            gameManager.PlaySound(SoundType.LostCatch);
        }
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (initDelayTimer > 0)
        {
            initDelayTimer -= Time.deltaTime;
            return;
        }

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
