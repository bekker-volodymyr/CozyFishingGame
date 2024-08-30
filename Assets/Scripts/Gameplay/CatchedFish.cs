using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchedFish : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private Vector3[] _path = {
        new Vector3(0.65f, -1.55f, 2.15f),
        new Vector3(-0.055f, -0.85f, 2.15f),
        new Vector3(-0.65f, -1.065f, 2.15f) };

    private void Start()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        gameObject.SetActive(false);
    }

    public void OnCatch(Sprite image)
    {
        _renderer.sprite = image;
        gameObject.SetActive(true);

        transform.DOPath(_path, 1f, PathType.CatmullRom).OnComplete(() => gameObject.SetActive(false));
    }
}
