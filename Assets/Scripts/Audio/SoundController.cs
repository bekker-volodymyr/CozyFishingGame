using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [Space] 
    [SerializeField] private Button soundButton;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    private bool isButtonOn = true;

    [Space]
    [SerializeField] private FishingManager gameManager;

    private float soundValue;

    public void OnButtonClick()
    {
        if (isButtonOn)
        {
            gameManager.SetVolume(0);
            soundButton.image.sprite = soundOff;
        }
        else
        {
            gameManager.SetVolume(soundValue);
            soundButton.image.sprite = soundOn;
        }

        isButtonOn = !isButtonOn;
    }

    public void OnSliderValueChanged(float value)
    {
        soundValue = value;
        if (soundValue > 0)
        {
            isButtonOn = true;
            soundButton.image.sprite = soundOn;
        }
        else
        {
            isButtonOn = false;
            soundButton.image.sprite = soundOff;
        }
        gameManager.SetVolume(soundValue);
    }

}
