using System;
using UnityEngine;
using UnityEngine.UI;

public class FishButton : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    private FishSO buttonFish;

    public event Action<FishSO> FishClickEvent;

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
        FishClickEvent?.Invoke(buttonFish);
    }
}
