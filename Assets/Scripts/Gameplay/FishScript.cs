using DG.Tweening;
using System.Collections;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    private float speed;

    private float minY = 0f;
    private float maxY;
    private float minJumpDistance;
    private float jumpPos;
    private float jumpStartPos;
    private int direction = 1; // 1 is up -1 is down

    private bool onDelay = false;

    public void SetValues(float speed, float maxY, float minJumpDistance)
    {
        this.speed = speed;

        this.minJumpDistance = minJumpDistance;

        this.maxY = maxY - rectTransform.rect.height / 2f;
        minY = 0f + rectTransform.rect.height / 2f;

        rectTransform.anchoredPosition = new Vector3(0f, minY, 0f);

    }

    public void InitJump()
    {
        GetJumpPosition();
        onDelay = false;
    }

    private void GetJumpPosition()
    {
        jumpStartPos = rectTransform.anchoredPosition.y;

        if (jumpStartPos < minY + minJumpDistance)
        {
            direction = 1;
        }
        else if (jumpStartPos > maxY - minJumpDistance)
        {
            direction = -1;
        }
        else
        {
            direction = Random.value > 0.5 ? 1 : -1;
        }


        jumpPos = direction == 1 ? Random.Range(rectTransform.anchoredPosition.y + minJumpDistance, maxY) : Random.Range(minY, rectTransform.anchoredPosition.y - minJumpDistance);
    }

    void Update()
    {
        if (!onDelay)
        {
            float newY = rectTransform.anchoredPosition.y + direction * speed * Time.deltaTime;

            if (direction == 1)
            {
                newY = Mathf.Clamp(newY, jumpStartPos, jumpPos);
            }
            else
            {
                newY = Mathf.Clamp(newY, jumpPos, jumpStartPos);
            }

            rectTransform.anchoredPosition = new Vector3(0f, newY, 0f);

            if (newY == jumpPos)
            {
                onDelay = true;
                StartCoroutine("Delay");
            }
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 0.9f));

        onDelay = false;
        InitJump();
    }
}
