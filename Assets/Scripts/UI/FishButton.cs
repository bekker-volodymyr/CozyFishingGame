using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishButton : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    private FishSO buttonFish;
    private bool isUnlocked = false;

    public event Action<FishSO, bool> FishClickEvent;

    public void InitButton(FishSO fishSO, bool isUnlocked)
    {
        buttonFish = fishSO;
        
        this.isUnlocked = isUnlocked;

        if (this.isUnlocked)
        {
            buttonImage.sprite = fishSO.Image;
        }
    }

    public void OnClick()
    {
        FishClickEvent?.Invoke(buttonFish, isUnlocked);
    }

    public void OnUnlockEvent(List<FishSO> list)
    {
        if(!isUnlocked && list.Contains(buttonFish))
        {
            buttonImage.sprite = buttonFish.Image;
            isUnlocked = true;
        }
    }
}
