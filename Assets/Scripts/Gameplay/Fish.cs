using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish
{
    private FishSO _fishType;
    private float _weight;
    public FishSO FishType
    {
        get { return _fishType; }
        set { _fishType = value; }
    }
    public float Weight
    {
        get { return _weight; }
        set { _weight = value; }
    }
}
