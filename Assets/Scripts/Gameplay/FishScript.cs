using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    private RectTransform rectTransform;

    private float speed = 150f;

    private float minY = -336f;
    private float maxY = 336;
    private float jumpPos;
    private float jumpStartPos;
    private int direction = 1; // 1 is up -1 is down

    private bool onDelay = false;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetValues(float speed)
    {
        this.speed = speed;
    }

    public void InitJump()
    {
        GetJumpPosition();
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
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));

        onDelay = false;
        GetJumpPosition();
    }
}
