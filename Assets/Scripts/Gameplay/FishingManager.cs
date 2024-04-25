using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    [SerializeField] private FishStorageSO fishes;

    void Update()
    {
        if(Input.touches.Length > 0)
        {
            Debug.Log(GenerateFish().Title);
        }
    }

    private FishSO GenerateFish()
    {
        return fishes.fishes[Random.Range(0, fishes.fishes.Count)];
    }
}
