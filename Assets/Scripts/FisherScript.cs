using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisherScript : MonoBehaviour
{
    public event Action castAnimationEnds;

    void OnCastAnimationEnds()
    {
        castAnimationEnds.Invoke();
    }
}
