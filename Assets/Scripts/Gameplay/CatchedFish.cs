using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchedFish : MonoBehaviour
{
    public void OnCatchAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
