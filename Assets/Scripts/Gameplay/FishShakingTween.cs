using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishShakingTween : MonoBehaviour
{
    private RectTransform _rectTransform;

    [Space]
    [SerializeField] private float _duration;
    [SerializeField] private float _strength;
    [SerializeField] private int _vibrato;
    [SerializeField] private float _randomness;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        StartShaking();
    }

    public void StartShaking()
    {
        _rectTransform.DOShakeAnchorPos(_duration, _strength, _vibrato, _randomness).SetLoops(-1);
    }
}
