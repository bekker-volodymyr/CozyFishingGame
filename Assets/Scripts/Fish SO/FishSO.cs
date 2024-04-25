using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FishSO : ScriptableObject
{
    public string Title;
    public string Description;
    public int Rarity;
    public float MaxWeight;
    public float MinWeight;
    public float PriceCoef;
}
