using UnityEngine;

public class CatchState : GameState
{
    private RectTransform fishTransform;
    private RectTransform rodTransform;

    private float successNewValue;

    private float increaseSpeed = 0.35f;
    private float decreaseSpeed = 0.55f;

    private float rodSpeed = 0.5f;

    private bool isColliding = true;

    private bool isTrembleUp = true;
    private float tremblinValue = 0.5f;

    public CatchState(StateMachine stateMachine, FishingManager gameManager) : base(stateMachine, gameManager)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        successNewValue = 0.01f;

        fishTransform = gameManager.FishIndicator.GetComponent<RectTransform>();
        rodTransform = gameManager.RodIndicator.GetComponent<RectTransform>();

        gameManager.SetCatchState();

        gameManager.rodScript.CollisionEnter += ToggleColliding;
        gameManager.rodScript.CollisionExit += ToggleColliding;
    }

    public override void ExitState()
    {
        base.ExitState();

        gameManager.rodScript.CollisionEnter -= ToggleColliding;
        gameManager.rodScript.CollisionExit -= ToggleColliding;
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

        if(Input.touchCount > 0)
        {
            rodTransform.Translate(Vector3.up * Time.deltaTime * rodSpeed);
        }
    }

    public override void FrameLateUpdate()
    {
        base.FrameLateUpdate();

        gameManager.SuccessIndicator.fillAmount = successNewValue;

        //FishTremblin();
    }

    private void ToggleColliding()
    {
        isColliding = !isColliding;
    }

    //private void FishTremblin()
    //{
    //    Vector3 currentPos = fishTransform.localPosition;
    //    tremblinValue *= isTrembleUp ? 1 : -1;
    //    fishTransform.localPosition = new Vector3(currentPos.x, currentPos.y - tremblinValue, currentPos.z);
    //    isTrembleUp = !isTrembleUp;
    //}
}
