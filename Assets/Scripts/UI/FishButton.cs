using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishButton : MonoBehaviour
{
    private Image buttonImage;
    private FishSO buttonFish;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void InitButton(FishSO fishSO)
    {
        buttonFish = fishSO;
        
        if (fishSO.isUnlocked)
        {
            buttonImage.sprite = fishSO.Image;
        }
    }

    public void OnClick()
    {
        Debug.Log($"Click on {buttonFish.Title}");
    }
}
