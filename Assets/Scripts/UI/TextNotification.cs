using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextNotification : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color _pullColor;
    [SerializeField] private Color _catchColor;
    [SerializeField] private Color _lostColor;

    [Space]
    [Range(0f, 1f)]
    [SerializeField] private float _showDuration = 0.6f;
    [Range(0f, 1f)]
    [SerializeField] private float _interval = 0.2f;
    [Range(0f, 1f)]
    [SerializeField] private float _hideDuration = 0.2f;

    private TextMeshProUGUI _textTMP;

    private void Start()
    {
        _textTMP = GetComponent<TextMeshProUGUI>();
    }

    public void ShowText(TextType type)
    {
        _textTMP.text = type.ToString().ToUpper();

        switch (type)
        {
            case TextType.Pull:
                _textTMP.color = _pullColor; break;
            case TextType.Catch:
                _textTMP.color = _catchColor; break;
            case TextType.Lost:
                _textTMP.color = _lostColor; break;
            default:
                Debug.LogWarning($"Unknown notification type: {type}");
                return;
        }

        Sequence seq = DOTween.Sequence();
        seq.Append(_textTMP.DOFade(1f, _showDuration).SetEase(Ease.InSine)).
            AppendInterval(_interval).
            Append(_textTMP.DOFade(0f, _hideDuration).SetEase(Ease.OutSine));
    }
}
