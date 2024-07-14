using System.Collections;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    private float speed;

    private float minY = 0f;
    private float maxY;
    private float jumpPos;
    private float jumpStartPos;
    private int direction = 1; // 1 is up -1 is down

    private bool onDelay = false;

    public void SetValues(float speed, float maxY)
    {
        this.speed = speed;

        this.maxY = maxY - rectTransform.rect.height / 2;
    }

    public void InitJump()
    {
        GetJumpPosition();
        onDelay = false;
    }

    private void GetJumpPosition()
    {
        jumpStartPos = rectTransform.localPosition.y;

        direction = Random.value > 0.5 ? 1  : -1;

        jumpPos = direction == 1 ? Random.Range(rectTransform.localPosition.y, maxY) : Random.Range(minY, rectTransform.localPosition.y);
    }

    void Update()
    {
        if (!onDelay)
        {
            float newY = rectTransform.localPosition.y + direction * speed * Time.deltaTime;

            if (direction == 1)
            {
                newY = Mathf.Clamp(newY, jumpStartPos, jumpPos);
            }
            else
            {
                newY = Mathf.Clamp(newY, jumpPos, jumpStartPos);
            }

            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, newY, rectTransform.localPosition.z);

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
