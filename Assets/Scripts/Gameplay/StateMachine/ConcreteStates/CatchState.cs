using UnityEngine;

public class CatchState : GameState
{
    private RectTransform fishTransform;
    private RectTransform rodTransform;

    private float successNewValue;

    private float increaseSpeed = 0.35f;
    private float decreaseSpeed = 0.55f;

    private float rodSpeed = 0.5f;

    private bool isColliding;

    public CatchState(StateMachine stateMachine, FishingManager gameManager) : base(stateMachine, gameManager)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        successNewValue = 0.01f;
        isColliding = true;

        fishTransform = gameManager.FishIndicator.GetComponent<RectTransform>();
        rodTransform = gameManager.RodIndicator.GetComponent<RectTransform>();

        gameManager.SetCatchState();

        gameManager.rodScript.CollisionEnter += SetCollidingTrue;
        gameManager.rodScript.CollisionExit += SetCollidingFalse;
    }

    public override void ExitState()
    {
        base.ExitState();

        gameManager.rodScript.CollisionEnter -= SetCollidingTrue;
        gameManager.rodScript.CollisionExit -= SetCollidingFalse;
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

        if (Input.touchCount > 0)
        {
            // Iterate through each touch
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // Check if the touch phase is either Began or Moved
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    // Move the rod only when there's a touch
                    rodTransform.Translate(Vector3.up * Time.deltaTime * rodSpeed);
                }
            }
        }
    }

    public override void FrameLateUpdate()
    {
        base.FrameLateUpdate();

        gameManager.SuccessIndicator.fillAmount = successNewValue;

        //FishTremblin();
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
